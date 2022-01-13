namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Bff.Application.Properties;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Factories;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using Bizca.Core.Domain;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
    {
        private readonly ICreateNewUserOutput createUserOutput;
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        private readonly IUserFactory userFactory;
        private readonly IUserWrapper userAgent;
        public CreateUserUseCase(IUserFactory userFactory,
            ICreateNewUserOutput createUserOutput,
            IUserRepository userRepository,
            IEventService eventService,
            IUserWrapper userAgent)
        {
            this.createUserOutput = createUserOutput;
            this.userRepository = userRepository;
            this.eventService = eventService;
            this.userFactory = userFactory;
            this.userAgent = userAgent;
        }

        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            UserRequest userRequest = GetUserRequest(command);
            User user = userFactory.Create(userRequest);
            await userRepository.AddAsync(user);

            UserToCreateRequest userToCreateRequest = MapTo(user);
            IPublicResponse<UserCreatedResponse> response = await userAgent.CreateUserAsync(userToCreateRequest);
            if (!response.Success)
            {
                createUserOutput.Invalid(response);
                return Unit.Value;
            }

            var CodeConfirmationRequest = new RegisterUserConfirmationCodeRequest(command.ExternalUserId,
                ChannelType.Email);

            var CodeConfirmationResponse = await userAgent.RegisterChannelConfirmationCodeAsync(CodeConfirmationRequest);
            if (!CodeConfirmationResponse.Success)
            {
                createUserOutput.Invalid(CodeConfirmationResponse);
                return Unit.Value;
            }

            string fullName = $"{command.FirstName} {command.LastName}";
            var sender = new MailAddressRequest(command.PartnerCode, Resources.BIZCA_NO_REPLY_EMAIL);
            var recipients = new List<MailAddressRequest>
            {
                new MailAddressRequest(fullName, command.Email)
            };
            var httpContent = GetHtmlContent(command.ExternalUserId,
                CodeConfirmationResponse.Data.ConfirmationCode,
                command.Email);

            user.RegisterSendEmailEvent(sender,
                recipients,
                Resources.EMAIL_CONFIRMATION_SUBJECT,
                httpContent);

            CreateNewUserDto newUserDto = MapTo(command.Role, response.Data);
            eventService.Enqueue(user.UserEvents);
            createUserOutput.Ok(newUserDto);
            return Unit.Value;
        }

        #region private helpers

        private string GetHtmlContent(string externalUserId, string codeConfirmation, string email)
        {
            string concatStr = $"{email}:{externalUserId}:{codeConfirmation}";
            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);
            string base64Str = Convert.ToBase64String(bytes);
            return $"<p><span style='color: #ffffff; font-weight: normal; vertical-align: middle; background-color: #0092ff; " +
                   $"border-radius: 15px; border: 0px None #000; padding: 8px 20px 8px 20px;'> <a style='text-decoration: none; " +
                   $"color: #ffffff; font-weight: normal;' target='_blank' rel='noreferrer'" +
                   $"href='https://integ-bizca-front.azurewebsites.net/create-password/{base64Str}'>Confirmer votre adresse email</a></span></p>";
        }
        private CreateNewUserDto MapTo(Role role, UserCreatedResponse response)
        {
            return new CreateNewUserDto(response.ExternalUserId,
                response.FirstName,
                response.LastName,
                response.Civility,
                role,
                response.Channels);
        }
        private UserRequest GetUserRequest(CreateUserCommand request)
        {
            return new UserRequest(request.ExternalUserId,
                request.PartnerCode,
                request.Civility,
                request.EconomicActivity,
                request.PhoneNumber,
                request.FirstName,
                request.LastName,
                request.Whatsapp,
                request.Email,
                request.Role);
        }
        private UserToCreateRequest MapTo(User user)
        {
            return new UserToCreateRequest(user.UserIdentifier.ExternalUserId,
                user.UserProfile.FirstName,
                user.UserProfile.LastName,
                (int)user.UserProfile.Civility,
                user.UserProfile.PhoneNumber,
                user.UserProfile.Whatsapp,
                user.UserProfile.Email);
        }

        #endregion
    }
}