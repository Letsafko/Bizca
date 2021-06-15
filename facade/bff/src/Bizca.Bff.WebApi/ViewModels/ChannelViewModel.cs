namespace Bizca.Bff.WebApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    internal sealed class ChannelViewModel
    {
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
