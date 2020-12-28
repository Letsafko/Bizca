namespace Bizca.User.WebApi.ViewModels
{
    using Bizca.User.Application.UseCases.GetUser.Common;

    /// <summary>
    ///     Gets user channel model.
    /// </summary>
    public sealed class UserChannelModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="UserChannelModel"/>
        /// </summary>
        /// <param name="channelDto"></param>
        public UserChannelModel(ChannelDto channelDto)
        {
            Active = channelDto.Active;
            Confirmed = channelDto.Confirmed;
            ChannelType = channelDto.ChannelType;
            ChannelValue = channelDto.ChannelValue;
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
