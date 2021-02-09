namespace Bizca.User.Domain.Agregates
{
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;

    public interface IUser
    {
        /// <summary>
        ///     Gets user code.
        /// </summary>
        public UserCode UserCode { get; }

        /// <summary>
        ///     Add a new channel.
        /// </summary>
        /// <param name="value">channel value.</param>
        /// <param name="channelType">channel type.</param>
        /// <param name="active">Indicates whether a channel is active.</param>
        /// <param name="confirmed">Indicates wheter a channel is confirmed.</param>
        void AddChannel(string value, ChannelType channelType, bool active, bool confirmed);

        /// <summary>
        ///     Gets channel by channel type.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        Channel GetChannel(ChannelType channelType);

        /// <summary>
        ///     Add new channel code confirmtion.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        void AddChannelCodeConfirmation(ChannelType channelType);

        /// <summary>
        ///     Update channel code confirmation.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        /// <param name="channelConfirmation">channel code confirmation.</param>
        void UpdateChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation);

        /// <summary>
        ///     Update channel.
        /// </summary>
        /// <param name="channelValue">channel value.</param>
        /// <param name="channelType">channel </param>
        /// <param name="active">Indicates whether a channel is active.</param>
        /// <param name="confirmed">Indicates whether a channel is confirmed.</param>
        void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed);
    }
}