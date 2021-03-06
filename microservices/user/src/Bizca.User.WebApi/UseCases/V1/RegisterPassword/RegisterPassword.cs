using System.ComponentModel.DataAnnotations;

namespace Bizca.User.WebApi.UseCases.V1.RegisterPassword
{
    /// <summary>
    ///     Register a new password.
    /// </summary>
    public sealed class RegisterPassword
    {
        /// <summary>
        ///     Sets password to hash.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Sets channel resource value.
        /// </summary>
        [Required]
        public string ChannelResource { get; set; }
    }
}