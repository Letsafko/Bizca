namespace Bizca.Bff.WebApi.ViewModels
{
    /// <summary>
    ///     user password.
    /// </summary>
    public sealed class UserPasswordViewModel
    {
        /// <summary>
        ///     Create a new instance of <see cref="UserPasswordViewModel"/>
        /// </summary>
        /// <param name="success"></param>
        public UserPasswordViewModel(bool success)
        {
            Success = success;
        }
        /// <summary>
        ///     Indicates whether a password has been upsert succesfully.
        /// </summary>
        public bool Success { get; }
    }
}
