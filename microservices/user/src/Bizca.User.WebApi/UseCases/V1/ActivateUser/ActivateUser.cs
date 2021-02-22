namespace Bizca.User.WebApi.UseCases.V1.ActivateUser
{
    /// <summary>
    ///     Activates or desactivates an user.
    /// </summary>
    public sealed class ActivateUser
    {
        /// <summary>
        ///     Indicates whether an user should be activate or desactivate.
        /// </summary>
        public string Activate { get; set; }
    }
}
