namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.Exceptions;
    using Bizca.User.Domain.Entities.Channel.Repositories;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ConfirmChannelCodeUseCase : ICommandHandler<ChannelConfirmationCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IUserRepository userRepository;
        private readonly IConfirmChannelCodeOutput output;
        private readonly IChannelRepository channelRepository;
        private readonly IReferentialService referentialService;
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
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound("user not found.");
                return Unit.Value;
            }

            Channel channel = response.GetChannel(request.ChannelType);
            ChannelConfirmation codeConfirmation = GetChannelConfirmation(channel, request.CodeConfirmation);
            if (codeConfirmation is null)
            {
                ThrowChannelCodeConfirmationDoesNotExistException(request.CodeConfirmation);
            }

            var user = response as User;
            bool confirmed = IsChannelCodeConfirmed(codeConfirmation);
            response.UpdateChannel(channel.ChannelValue, channel.ChannelType, channel.Active, confirmed);

            await userRepository.UpdateAsync(user).ConfigureAwait(false);
            await channelRepository.UpSertAsync(user.Id, user.Channels).ConfigureAwait(false);

            output.Ok(new ConfirmChannelCodeDto(channel.ChannelType, channel.ChannelValue, confirmed));
            return Unit.Value;
        }

        #region private helpers

        private ChannelConfirmation GetChannelConfirmation(Channel channel, string confirmationCode)
        {
            return channel.ChannelCodes.SingleOrDefault(x => x.CodeConfirmation == confirmationCode);
        }

        private bool IsChannelCodeConfirmed(ChannelConfirmation channelConfirmation)
        {
            bool confirmed = channelConfirmation.ExpirationDate.CompareTo(DateTime.UtcNow) > 0;
            if (!confirmed)
            {
                var failure = new DomainFailure($"{channelConfirmation.CodeConfirmation} has expired.",
                    nameof(channelConfirmation.CodeConfirmation),
                    typeof(ChannelCodeConfirmationHasExpiredUserException));

                throw new ChannelCodeConfirmationHasExpiredUserException(new List<DomainFailure> { failure });
            }
            return confirmed;
        }

        private void ThrowChannelCodeConfirmationDoesNotExistException(string codeConfirmation)
        {
            var failure = new DomainFailure($"{codeConfirmation} does not exist.",
                    nameof(codeConfirmation),
                    typeof(ChannelCodeConfirmationDoesNotExistException));

            throw new ChannelCodeConfirmationDoesNotExistException(new List<DomainFailure> { failure });
        }

        #endregion
    }
}