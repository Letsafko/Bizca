namespace Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode
{
    /// <summary>
    ///     Creates a channel code confirmation.
    /// </summary>
    public sealed class RegisterCodeConfirmation
    {
        /// <summary>
        ///     Notification channel type.
        /// </summary>
        public string Channel { get; set; }
    }
}