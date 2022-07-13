namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Bff.Domain;
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
    using Bizca.Core.Domain.EmailTemplate;
    using Bizca.Core.Domain.Services;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
    {
        private readonly ICreateNewUserOutput createUserOutput;
        private readonly IReferentialService referentialService;
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        private readonly IUserFactory userFactory;
        private readonly IUserWrapper userAgent;
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

            var CodeConfirmationRequest = new RegisterUserConfirmationCodeRequest(command.ExternalUserId, ChannelType.Email);
            var CodeConfirmationResponse = await userAgent.RegisterChannelConfirmationCodeAsync(CodeConfirmationRequest);
            if (!CodeConfirmationResponse.Success)
            {
                createUserOutput.Invalid(CodeConfirmationResponse);
                return Unit.Value;
            }

            var emailTemplate = await referentialService.GetEmailTemplateByIdAsync((int)EmailTemplateType.AccountActivation, true);
            var recipients = new List<MailAddressRequest>
            {
                new MailAddressRequest(command.Email)
            };

            var parameters = GetParameters(command, CodeConfirmationResponse.Data.ConfirmationCode);
            user.RegisterSendTransactionalEmailEvent(recipients: recipients,
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
            var token = $"{command.Email}:{command.ExternalUserId}:{codeConfirmation}";
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var tokenBase64 = Convert.ToBase64String(tokenBytes);


            var activateUserUrl = $"https://integ-bizca-front.azurewebsites.net/create-password/{tokenBase64}";
            return new Dictionary<string, object>
            {
                [AttributeConstant.Parameter.ActivateUserUrl] = activateUserUrl
            };
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