namespace Bizca.Bff.WebApi.ViewModels
{
    /// <summary>
    ///     user password.
    /// </summary>
    public sealed class PasswordViewModel
    {
        /// <summary>
        ///     Create a new instance of <see cref="PasswordViewModel"/>
        /// </summary>
        /// <param name="success"></param>
        public PasswordViewModel(bool success)
        {
            Success = success;
        }
        /// <summary>
        ///     Indicates whether a password has been upsert succesfully.
        /// </summary>
        public bool Success { get; }
    }
}
