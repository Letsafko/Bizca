namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.Repositories;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RegisterCodeConfirmationUseCase : ICommandHandler<RegisterCodeConfirmationCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IUserRepository userRepository;
        private readonly IReferentialService referentialService;
        private readonly IRegisterCodeConfirmationOutput output;
        private readonly IChannelConfirmationRepository channelConfirmationRepository;
        public RegisterCodeConfirmationUseCase(IUserFactory userFactory,
            IReferentialService referentialService,
            IRegisterCodeConfirmationOutput output,
            IUserRepository userRepository,
            IChannelConfirmationRepository channelConfirmationRepository)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.userRepository = userRepository;
            this.referentialService = referentialService;
            this.channelConfirmationRepository = channelConfirmationRepository;
        }

        /// <summary>
        ///     Handles registration of user channel code confirmation.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(RegisterCodeConfirmationCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound($"user::{request.ExternalUserId} does not exist.");
                return Unit.Value;
            }

            var user = response as User;
            user.AddChannelCodeConfirmation(request.ChannelType);
            await userRepository.UpdateAsync(user).ConfigureAwait(false);

            IReadOnlyCollection<ChannelConfirmation> channelCodes = user.GetChannel(request.ChannelType).ChannelCodes;
            await channelConfirmationRepository.UpsertAsync(user.Id, request.ChannelType, channelCodes).ConfigureAwait(false);

            RegisterCodeConfirmationDto registerCode = GetChannel(request.ChannelType, user);
            output.Ok(registerCode);
            return Unit.Value;
        }

        private RegisterCodeConfirmationDto GetChannel(ChannelType channelType, User user)
        {
            Channel channel = user.GetChannel(channelType);
            return new RegisterCodeConfirmationDto(channel.ChannelType.Code,
                    channel.ChannelValue,
                    channel.ChannelCodes.OrderByDescending(x => x.ExpirationDate).First().CodeConfirmation);
        }
    }
}