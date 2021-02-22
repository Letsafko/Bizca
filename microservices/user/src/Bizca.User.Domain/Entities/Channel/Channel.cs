namespace Bizca.User.Domain.Entities.Channel
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Channel : Entity
    {
        public Channel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            Active = active;
            ChannelValue = value;
            Confirmed = confirmed;
            ChannelType = channelType;
            channelCodes = new List<ChannelConfirmation>();
        }

        public bool Active { get; private set; }
        public bool Confirmed { get; private set; }
        public string ChannelValue { get; private set; }
        public ChannelType ChannelType { get; }
        public IReadOnlyCollection<ChannelConfirmation> ChannelCodes => channelCodes.ToList();
        private readonly ICollection<ChannelConfirmation> channelCodes;

        /// <summary>
        ///     Add new channel code confirmation.
        /// </summary>
        /// <param name="channelConfirmation"></param>
        internal void AddCodeConfirmation(ChannelConfirmation channelConfirmation)
        {
            channelCodes.Add(channelConfirmation);
        }

        /// <summary>
        ///     Update channel.
        /// </summary>
        /// <param name="channelValue"></param>
        /// <param name="channelActive"></param>
        /// <param name="channelConfirmed"></param>
        internal void UpdateChannel(string channelValue, bool channelActive, bool channelConfirmed)
        {
            Active = channelActive;
            ChannelValue = channelValue;
            Confirmed = channelConfirmed;
        }
    }
}