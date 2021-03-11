using System.ComponentModel.DataAnnotations;

namespace Bizca.User.WebApi.UseCases.V1.RegisterPassword
{
    /// <summary>
    ///     Register a new password.
    /// </summary>
    public sealed class RegisterPassword
    {
        /// <summary>
        ///     Gets or sets password to hash.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets channel resource value.
        /// </summary>
        [Required]
        public string Resource { get; set; }
    }
}
