namespace Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Confirm channel code.
    /// </summary>
    public sealed class ConfirmChannelCode
    {
        /// <summary>
        ///     Channel confirmation code.
        /// </summary>
        [Required]
        public string ConfirmationCode { get; set; }
    }
}