namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Core.Application.Commands;
    using Core.Application.Services;
    using Core.Domain;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using Domain;
    using Domain.Entities.User;
    using Domain.Entities.User.Factories;
    using Domain.Enumerations;
    using Domain.Wrappers.Notification.Requests.Email;
    using Domain.Wrappers.Users;
    using Domain.Wrappers.Users.Requests;
    using Domain.Wrappers.Users.Responses;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
    {
        private readonly ICreateNewUserOutput createUserOutput;
        private readonly IEventService eventService;
        private readonly IReferentialService referentialService;
        private readonly IUserWrapper userAgent;
        private readonly IUserFactory userFactory;
        private readonly IUserRepository userRepository;

        public CreateUserUseCase(IUserFactory userFactory,
            ICreateNewUserOutput createUserOutput,
            IUserRepository userRepository,
            IReferentialService referentialService,
            IEventService eventService,
            IUserWrapper userAgent)
        {
            this.referentialService = referentialService;
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

            var CodeConfirmationRequest =
                new RegisterUserConfirmationCodeRequest(command.ExternalUserId, ChannelType.Email);
            IPublicResponse<RegisterUserConfirmationCodeResponse> CodeConfirmationResponse =
                await userAgent.RegisterChannelConfirmationCodeAsync(CodeConfirmationRequest);
            if (!CodeConfirmationResponse.Success)
            {
                createUserOutput.Invalid(CodeConfirmationResponse);
                return Unit.Value;
            }

            EmailTemplate emailTemplate =
                await referentialService.GetEmailTemplateByIdAsync((int)EmailTemplateType.AccountActivation, true);
            var recipients = new List<MailAddressRequest> { new MailAddressRequest(command.Email) };

            IDictionary<string, object> parameters =
                GetParameters(command, CodeConfirmationResponse.Data.ConfirmationCode);
            user.RegisterSendTransactionalEmailEvent(recipients,
                emailTemplate: emailTemplate.EmailTemplateId,
                parameters: parameters);

            CreateNewUserDto newUserDto = MapTo(command.Role, response.Data);
            eventService.Enqueue(user.UserEvents);
            createUserOutput.Ok(newUserDto);
            return Unit.Value;
        }

        #region private helpers

        private static IDictionary<string, object> GetParameters(CreateUserCommand command,
            string codeConfirmation)
        {
            string token = $"{command.Email}:{command.ExternalUserId}:{codeConfirmation}";
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            string tokenBase64 = Convert.ToBase64String(tokenBytes);


            string activateUserUrl = $"https://integ-bizca-front.azurewebsites.net/create-password/{tokenBase64}";
            return new Dictionary<string, object> { [AttributeConstant.Parameter.ActivateUserUrl] = activateUserUrl };
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