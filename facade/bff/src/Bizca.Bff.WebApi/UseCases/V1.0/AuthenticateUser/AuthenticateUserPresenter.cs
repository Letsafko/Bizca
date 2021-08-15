namespace Bizca.Bff.WebApi.UseCases.V10.AuthenticateUser
{
    using Bizca.Bff.Application.UseCases.AuthenticateUser;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Authenticate user presenter
    /// </summary>
    public sealed class AuthenticateUserPresenter : IAuthenticateUserOutput
    {
        /// <summary>
        ///     Authenticate user view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        public void Ok(AuthenticateUserDto authenticateUser)
        {
            ViewModel = new OkObjectResult(new UserViewModel(authenticateUser));
        }
    }
}