﻿namespace Bizca.User.Domain.Entities.Channel
{
    using Core.Domain;
    using System.Collections.Generic;
    using ValueObjects;

    public sealed class Channel : Entity
    {
        private readonly HashSet<ChannelConfirmation> channelCodes;

        public Channel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            Active = active;
            ChannelValue = value;
            Confirmed = confirmed;
            ChannelType = channelType;
            channelCodes = new HashSet<ChannelConfirmation>();
        }

        public bool Active { get; private set; }
        public bool Confirmed { get; private set; }
        public string ChannelValue { get; private set; }
        public ChannelType ChannelType { get; }
        public IReadOnlyCollection<ChannelConfirmation> ChannelCodes => channelCodes;

        /// <summary>
        ///     Add new channel code confirmation.
        /// </summary>
        /// <param name="channelConfirmation"></param>
        internal void AddNewCodeConfirmation(ChannelConfirmation channelConfirmation)
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
            Confirmed = channelConfirmed;
            ChannelValue = channelValue;
            Active = channelActive;
        }
    }
}