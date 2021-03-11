namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    ///     Confirmation channel code.
    /// </summary>
    public sealed class ConfirmChannelCode
    {
        /// <summary>
        ///     Notification channel.
        /// </summary>
        [Required]
        public string Channel { get; set; }

        /// <summary>
        ///     Notification channel code confirmation.
        /// </summary>
        [Required]
        public string CodeConfirmation { get; set; }
    }
}