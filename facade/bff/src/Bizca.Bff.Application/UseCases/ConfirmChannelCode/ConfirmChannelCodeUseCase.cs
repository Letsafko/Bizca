namespace Bizca.Bff.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ConfirmChannelCodeUseCase : ICommandHandler<ConfirmChannelCodeCommand>
    {
        private readonly IUserChannelWrapper userChanelAgent;
        private readonly IConfirmChannelCodeOutput output;
        private readonly IUserRepository userRepository;
        public ConfirmChannelCodeUseCase(IUserRepository userRepository,
            IConfirmChannelCodeOutput output,
            IUserWrapper userAgent)
        {
            this.userRepository = userRepository;
            this.userChanelAgent = userAgent;
            this.output = output;
        }

        public async Task<Unit> Handle(ConfirmChannelCodeCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(request.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");
            }

            user.SetChannelConfirmationStatus(ChannelConfirmationStatus.EmailConfirmed);
            await userRepository.UpdateAsync(user);

            var confirmationCodeRequest = new UserConfirmationCodeRequest(request.ConfirmationCode, request.ChannelType);
            UserConfirmationCodeResponse response = await userChanelAgent.ConfirmUserChannelCodeAsync(request.ExternalUserId, confirmationCodeRequest);
            output.Ok(response.ResourceId, response.Resource, response.Confirmed);
            return Unit.Value;
        }
    }
}