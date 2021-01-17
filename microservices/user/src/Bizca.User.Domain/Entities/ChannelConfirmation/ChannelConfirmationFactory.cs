namespace Bizca.User.Domain.Agregates.Factories
{
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.ChannelConfirmation;
    using Bizca.User.Domain.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ChannelConfirmationFactory : IChannelConfirmationFactory
    {
        private const int defaultCodeConfirmationExpirationDelay = 10;
        private const string defaultCodeConfirmationPattern = "000-000";
        private const string missingChannelDefaultMessagePattern = "channel::{0} requested for user::{1} does not exist.";

        public ChannelConfirmation Create(string externalUserId, ChannelType requestedChannelType, IEnumerable<ChannelType> channelTypes)
        {
            ChannelType channelType = GetChannelType(requestedChannelType, channelTypes);
            if (channelType is null)
            {
                var failure = new DomainFailure(string.Format(missingChannelDefaultMessagePattern, channelType.Code, externalUserId),
                        nameof(channelType),
                        typeof(ChannelDoesNotExistForUserException));

                throw new ChannelDoesNotExistForUserException(new List<DomainFailure> { failure });
            }

            string randomCode = GetRandomCode(defaultCodeConfirmationPattern);
            DateTime expirationDate = DateTime.UtcNow.AddMinutes(defaultCodeConfirmationExpirationDelay);
            return new ChannelConfirmation(channelType, randomCode, expirationDate);
        }

        #region private helpers

        private string GetRandomCode(string pattern)
        {
            return new Random().Next(0, 1000000).ToString(pattern);
        }

        private ChannelType GetChannelType(ChannelType channelType, IEnumerable<ChannelType> channelTypes)
        {
            return channelTypes.SingleOrDefault(x => x == channelType);
        }

        #endregion
    }
}