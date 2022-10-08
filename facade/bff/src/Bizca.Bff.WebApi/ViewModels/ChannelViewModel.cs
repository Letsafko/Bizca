namespace Bizca.Bff.WebApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Channel view model.
    /// </summary>
    public sealed class ChannelViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="ChannelViewModel" />
        /// </summary>
        /// <param name="channelValue">Channel value</param>
        /// <param name="channelType">Channel type</param>
        /// <param name="confirmed">Indicates whether channel is confirmed or not.</param>
        /// <param name="active">Indicates whether channel is active or not.</param>
        public ChannelViewModel(string channelValue,
            string channelType,
            bool confirmed,
            bool active)
        {
            ChannelValue = channelValue;
            ChannelType = channelType;
            Confirmed = confirmed;
            Active = active;
        }

        /// <summary>
        ///     Channel value.
        /// </summary>
        [Required]
        public string ChannelValue { get; set; }

        /// <summary>
        ///     Channel type.
        /// </summary>
        [Required]
        public string ChannelType { get; set; }

        /// <summary>
        ///     Indicates whether channel is confirmed.
        /// </summary>
        [Required]
        public bool Confirmed { get; set; }


        /// <summary>
        ///     Indicates whether channel is active.
        /// </summary>
        [Required]
        public bool Active { get; set; }
    }
}