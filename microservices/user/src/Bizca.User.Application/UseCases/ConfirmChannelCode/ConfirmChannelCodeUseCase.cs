namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.ChannelConfirmation;
    using Bizca.User.Domain.ValueObjects;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ConfirmChannelCodeUseCase : ICommandHandler<ChannelConfirmationCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IConfirmChannelCodeOutput output;
        private readonly IChannelRepository channelRepository;
        private readonly IReferentialService referentialService;
        private readonly IChannelConfirmationRepository channelConfirmationRepository;
        public ConfirmChannelCodeUseCase(IUserFactory userFactory,
            IConfirmChannelCodeOutput output,
            IReferentialService referentialService,
            IChannelRepository channelRepository,
            IChannelConfirmationRepository channelConfirmationRepository)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.channelRepository = channelRepository;
            this.referentialService = referentialService;
            this.channelConfirmationRepository = channelConfirmationRepository;
        }

        private const string missingChannelDefaultMessagePattern = "channel::{0} requested for user::{1} does not exist.";

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

            var user = response as User;
            Channel channel = GetChannel(request.ChannelType, user);
            DateTime? expirationDate = await GetCodeConfirmationExpirationDateAsync(user.Id, channel.ChannelType, request.CodeConfirmation).ConfigureAwait(false);

            if (!expirationDate.HasValue)
            {
                output.NotFound("code does not exist.");
                return Unit.Value;
            }

            if (expirationDate.Value.CompareTo(DateTime.UtcNow) < 0)
            {
                var failure = new DomainFailure($"{request.CodeConfirmation} has expired.", nameof(request.CodeConfirmation), typeof(ChannelCodeConfirmationHasExpiredUserException));
                throw new ChannelCodeConfirmationHasExpiredUserException(new List<DomainFailure> { failure });
            }

            var channelToUpdate = new Channel(channel.ChannelValue, channel.ChannelType, channel.Active, true);
            await channelRepository.UpdateAsync(user.Id, new List<Channel> { channelToUpdate }).ConfigureAwait(false);
            output.Ok(new ConfirmChannelCodeDto(channelToUpdate.ChannelType, channelToUpdate.ChannelValue, channelToUpdate.Confirmed));
            return Unit.Value;
        }

        #region private helpers

        private async Task<DateTime?> GetCodeConfirmationExpirationDateAsync(int userId, ChannelType channelType, string confirmationCode)
        {
            dynamic result = await channelConfirmationRepository.GetByIdsAsync(userId, channelType.Id, confirmationCode).ConfigureAwait(false);
            return result is null
                ? default(DateTime?)
                : (DateTime)result.expirationDate;
        }

        private Channel GetChannel(ChannelType channelType, User user)
        {
            Channel channel = user.Channels.SingleOrDefault(x => x.ChannelType == channelType);
            if (channel is null)
            {
                var failure = new DomainFailure(string.Format(missingChannelDefaultMessagePattern, channelType.Code, user.ExternalUserId.ToString()),
                        nameof(channelType),
                        typeof(ChannelDoesNotExistForUserException));

                throw new ChannelDoesNotExistForUserException(new List<DomainFailure> { failure });
            }

            return channel;
        }

        #endregion
    }
}