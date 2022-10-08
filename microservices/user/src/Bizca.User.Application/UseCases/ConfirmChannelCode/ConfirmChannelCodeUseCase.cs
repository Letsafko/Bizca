namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Core.Application.Commands;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using Domain.Agregates;
    using Domain.Agregates.Factories;
    using Domain.Agregates.Repositories;
    using Domain.Entities.Channel;
    using Domain.Entities.Channel.Repositories;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ConfirmChannelCodeUseCase : ICommandHandler<ChannelConfirmationCommand>
    {
        private readonly IChannelRepository channelRepository;
        private readonly IConfirmChannelCodeOutput output;
        private readonly IReferentialService referentialService;
        private readonly IUserFactory userFactory;
        private readonly IUserRepository userRepository;

        public ConfirmChannelCodeUseCase(IUserFactory userFactory,
            IUserRepository userRepository,
            IConfirmChannelCodeOutput output,
            IChannelRepository channelRepository,
            IReferentialService referentialService)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.userRepository = userRepository;
            this.channelRepository = channelRepository;
            this.referentialService = referentialService;
        }

        /// <summary>
        ///     Handles confirmation of a channel.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(ChannelConfirmationCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true)
                .ConfigureAwait(false);
            IUser response = await userFactory.BuildByPartnerAndExternalUserIdAsync(partner, request.ExternalUserId)
                .ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound($"no user associated to '{request.ExternalUserId}' exists.");
                return Unit.Value;
            }

            var user = response as User;
            bool confirmed = user.IsChannelCodeConfirmed(request.ChannelType, request.CodeConfirmation);
            Channel channel = user.GetChannel(request.ChannelType);

            user.UpdateChannel(channel.ChannelValue, channel.ChannelType, channel.Active, confirmed);
            await userRepository.UpdateAsync(user).ConfigureAwait(false);
            await channelRepository.UpSertAsync(user.UserIdentifier.UserId, user.Profile.Channels)
                .ConfigureAwait(false);

            output.Ok(new ConfirmChannelCodeDto(channel.ChannelType, channel.ChannelValue, confirmed));
            return Unit.Value;
        }
    }
}