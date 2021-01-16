namespace Bizca.User.Domain.Agregates.Users.Factories
{
    using Bizca.User.Domain.Agregates.Users.Exceptions;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class UserChannelConfirmationFactory : IUserChannelConfirmationFactory
    {
        private readonly IUserChannelConfirmationRepository userChannelConfirmationRepository;
        public UserChannelConfirmationFactory(IUserChannelConfirmationRepository userChannelConfirmationRepository)
        {
            this.userChannelConfirmationRepository = userChannelConfirmationRepository;
        }

        private const int defaultCodeConfirmationExpirationDelay = 10;
        private const string defaultCodeConfirmationPattern = "000-000";
        private const string missingChannelDefaultMessagePattern = "channel::{0} requested for user::{1} does not exist.";

        public ChannelCodeConfirmation Create(NotificationChanels requestedChannel, User user)
        {
            Channel channel = GetChannel(requestedChannel, user.Channels);
            if (channel is null)
            {
                throw new ChannelDoesNotExistForUserException(string.Format(missingChannelDefaultMessagePattern, requestedChannel.ToString(), user.ExternalUserId.ToString()));
            }

            string randomCode = GetRandomCode(defaultCodeConfirmationPattern);
            DateTime expirationDate = DateTime.UtcNow.AddMinutes(defaultCodeConfirmationExpirationDelay);
            return new ChannelCodeConfirmation(channel.ChannelType, channel.ChannelValue, randomCode, expirationDate);
        }

        #region private helpers

        private string GetRandomCode(string pattern)
        {
            return new Random().Next(0, 1000000).ToString(pattern);
        }

        private Channel GetChannel(NotificationChanels requestedChannel, ICollection<Channel> channels)
        {
            return channels.SingleOrDefault(x => x.ChannelType == requestedChannel);
        }

        #endregion
    }
}
