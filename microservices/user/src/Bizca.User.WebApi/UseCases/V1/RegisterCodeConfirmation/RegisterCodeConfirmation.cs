namespace Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    ///     Creates a channel code confirmation.
    /// </summary>
    public sealed class RegisterCodeConfirmation
    {
        /// <summary>
        ///     Notification channel type.
        /// </summary>
        [Required]
        public string Channel { get; set; }
    }
}