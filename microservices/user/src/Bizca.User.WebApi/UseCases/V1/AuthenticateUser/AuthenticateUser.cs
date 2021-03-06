namespace Bizca.User.WebApi.UseCases.V1.AuthenticateUser
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    ///     Authenticate user.
    /// </summary>
    public sealed class AuthenticateUser
    {
        /// <summary>
        ///     Gets user password to authenticate.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Gets channel resource value.
        /// </summary>
        [Required]
        public string Resource { get; set; }
    }
}