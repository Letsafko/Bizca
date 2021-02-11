namespace Bizca.User.WebApi.ViewModels
{
    using Bizca.User.Domain.Entities.Channel;

    /// <summary>
    ///     Gets user channel model.
    /// </summary>
    public sealed class UserChannelModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="UserChannelModel"/>
        /// </summary>
        /// <param name="channel"></param>
        public UserChannelModel(Channel channel)
        {
            Active = channel.Active;
            Confirmed = channel.Confirmed;
            ChannelValue = channel.ChannelValue;
            ChannelType = channel.ChannelType.Code.ToLower();
        }

        /// <summary>
        ///     Defines whether a channel is active or not.
        /// </summary>
        public bool Active { get; }

        /// <summary>
        ///     Defines whether a channel is confirmed or not.
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