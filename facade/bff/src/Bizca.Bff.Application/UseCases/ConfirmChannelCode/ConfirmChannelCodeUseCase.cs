namespace Bizca.Bff.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Bff.Domain.Entities.User;
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
        private readonly IConfirmChannelCodeOutput output;
        private readonly IUserRepository userRepository;
        private readonly IUserWrapper userAgent;
        public ConfirmChannelCodeUseCase(IUserRepository userRepository,
            IConfirmChannelCodeOutput output,
            IUserWrapper userAgent)
        {
            this.userRepository = userRepository;
            this.userAgent = userAgent;
            this.output = output;
        }

        public async Task<Unit> Handle(ConfirmChannelCodeCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetAsync(request.ExternalUserId);
            user.SetChannelConfirmationStatus(ChannelConfirmationStatus.EmailConfirmed);
            await userRepository.UpdateAsync(user);

            var confirmationCodeRequest = new UserConfirmationCodeRequest(request.ConfirmationCode, request.ChannelType);
            UserConfirmationCodeResponse response = await userAgent.ConfirmUserChannelCodeAsync(request.ExternalUserId, confirmationCodeRequest);
            output.Ok(response.ResourceId, response.Resource, response.Confirmed);
            return Unit.Value;
        }
    }
}