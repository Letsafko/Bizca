namespace Bizca.Bff.WebApi.UseCases.V10.AuthenticateUser
{
    /// <summary>
    ///     Authenticate user.
    /// </summary>
    public sealed class AuthenticateUser
    {
        /// <summary>
        ///     User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     User channe resource value
        /// </summary>
        public string Resource { get; set; }
    }
}