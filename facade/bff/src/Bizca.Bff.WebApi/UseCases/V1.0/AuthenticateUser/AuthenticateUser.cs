namespace Bizca.Bff.WebApi.UseCases.V1._0.AuthenticateUser
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