namespace Bizca.Bff.WebApi.UseCases.V10.CreateOrUpdateUserPassword
{
    /// <summary>
    ///     Create or update user password.
    /// </summary>
    public sealed class CreateOrUpdateUserPassword
    {
        /// <summary>
        ///     User password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     User channel resource.
        /// </summary>
        public string Resource { get; set; }
    }
}