namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Exceptions;
    using Bizca.User.Domain.Agregates.Users.Factories;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ConfirmChannelCodeUseCase : ICommandHandler<ConfirmChannelCodeCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IConfirmChannelCodeOutput output;
        private readonly IPartnerRepository partnerRepository;
        private readonly IUserChannelRepository userChannelRepository;
        private readonly IUserChannelConfirmationRepository userChannelConfirmationRepository;
        public ConfirmChannelCodeUseCase(IUserFactory userFactory,
            IConfirmChannelCodeOutput output,
            IPartnerRepository partnerRepository,
            IUserChannelRepository userChannelRepository,
            IUserChannelConfirmationRepository userChannelConfirmationRepository)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.partnerRepository = partnerRepository;
            this.userChannelRepository = userChannelRepository;
            this.userChannelConfirmationRepository = userChannelConfirmationRepository;
        }

        private const string missingChannelDefaultMessagePattern = "channel::{0} requested for user::{1} does not exist.";

        /// <summary>
        ///     Handles confirmation of a channel.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(ConfirmChannelCodeCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await partnerRepository.GetByCodeAsync(request.PartnerCode).ConfigureAwait(false);
            if (partner is null)
            {
                request.ModelState.Add(nameof(request.PartnerCode), "partner code is invalid.");
            }

            if (!request.ModelState.IsValid)
            {
                output.Invalid(request.ModelState);
                return Unit.Value;
            }

            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound("user not found.");
                return Unit.Value;
            }

            var user = response as User;
            Channel channel = GetChannel(request.ChannelId, user);
            DateTime? expirationDate = await GetCodeConfirmationExpirationDateAsync(user.Id, channel.ChannelType, request.CodeConfirmation).ConfigureAwait(false);

            if(!expirationDate.HasValue)
            {
                output.NotFound("code does not exist.");
                return Unit.Value;
            }

            if (expirationDate.Value.CompareTo(DateTime.UtcNow) > 0)
            {
                var channelToUpdate = new Channel(channel.ChannelValue, channel.ChannelType, channel.Active, true);
                await userChannelRepository.UpdateAsync(user.Id, new List<Channel> { channelToUpdate }).ConfigureAwait(false);
                output.Ok(new ConfirmChannelCodeDto(channelToUpdate.ChannelType, channelToUpdate.ChannelValue, channelToUpdate.Confirmed));
                return Unit.Value;
            }
            else
            {
                request.ModelState.Add(nameof(request.CodeConfirmation), "code has expired.");
                output.Invalid(request.ModelState);
                return Unit.Value;
            }
        }

        #region private helpers

        private async Task<DateTime?> GetCodeConfirmationExpirationDateAsync(int userId, NotificationChanels channelId, string confirmationCode)
        {
            dynamic result = await userChannelConfirmationRepository.GetByIdsAsync(userId, (int)channelId, confirmationCode).ConfigureAwait(false);
            return result is null
                ? default(DateTime?)
                : (DateTime)result.expirationDate;
        }

        private Channel GetChannel(NotificationChanels channelId, User user)
        {
            return user.Channels.SingleOrDefault(x => x.ChannelType == channelId)
                ?? throw new ChannelDoesNotExistForUserException(string.Format(missingChannelDefaultMessagePattern, channelId.ToString(), user.ExternalUserId.ToString()));
        }

        #endregion
    }
}
