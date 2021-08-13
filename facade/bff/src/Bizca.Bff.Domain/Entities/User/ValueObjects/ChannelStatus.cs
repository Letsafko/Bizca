namespace Bizca.Bff.Domain.Entities.User.ValueObjects
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class ChannelStatus : ValueObject
    {
        public ChannelStatus(ChannelConfirmationStatus channelConfirmationStatus,
            ChannelActivationStatus channelActivationStatus)
        {
            ChannelConfirmationStatus = channelConfirmationStatus;
            ChannelActivationStatus = channelActivationStatus;
        }
        public ChannelConfirmationStatus ChannelConfirmationStatus { get; }
        public ChannelActivationStatus ChannelActivationStatus { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ChannelConfirmationStatus;
            yield return ChannelActivationStatus;
        }
    }
}