namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
    /// <summary>
    ///     Confirmation channel code.
    /// </summary>
    public sealed class ConfirmChannelCode
    {
        /// <summary>
        ///     Notification channel.
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        ///     Notification channel code confirmation.
        /// </summary>
        public string CodeConfirmation { get; set; }
    }
}