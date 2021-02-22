namespace Bizca.User.WebApi.UseCases.V1.AuthenticateUser
{
    /// <summary>
    ///     Authenticate user.
    /// </summary>
    public sealed class AuthenticateUser
    {
        /// <summary>
        ///     Gets or sets password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets resource login.
        /// </summary>
        public string Resource { get; set; }
    }
}
