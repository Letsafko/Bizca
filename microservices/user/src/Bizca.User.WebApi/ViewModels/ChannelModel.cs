namespace Bizca.User.WebApi.ViewModels
{
    using Bizca.User.Domain.Entities.Channel;

    /// <summary>
    ///     Gets user channel model.
    /// </summary>
    public sealed class ChannelModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="ChannelModel"/>
        /// </summary>
        /// <param name="channel"></param>
        public ChannelModel(Channel channel)
        {
            ChannelType = channel.ChannelType.Description.ToLower();
            ChannelValue = channel.ChannelValue;
            Confirmed = channel.Confirmed;
            Active = channel.Active;
        }

        /// <summary>
        ///     Indicates whether a channel is active or not.
        /// </summary>
        public bool Active { get; }

        /// <summary>
        ///     Indicates whether a channel is confirmed or not.
        /// </summary>
        public bool Confirmed { get; }

        /// <summary>
        ///     Gets channel type.
        /// </summary>
        public string ChannelType { get; }

        /// <summary>
        ///     Get channel value.
        /// </summary>
        public string ChannelValue { get; }
    }
}