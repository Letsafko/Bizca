namespace Bizca.User.WebApi.UseCases.V1.AuthenticateUser
{
    using Bizca.User.Application.UseCases.AuthenticateUser;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Update password presenter.
    /// </summary>
    public sealed class AuthenticateUserPresenter : IAuthenticateUserOutput
    {
        /// <summary>
        ///     Gets view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Password updated.
        /// </summary>
        /// <param name="authenticateUserDto"></param>
        public void Ok(AuthenticateUserDto authenticateUserDto)
        {
            ViewModel = new OkObjectResult(new AuthenticateUserResponse(authenticateUserDto));
        }

        /// <summary>
        ///     User not found.
        /// </summary>
        /// <param name="message"></param>
        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }

        /// <summary>
        ///     Invalid request.
        /// </summary>
        /// <param name="message"></param>
        public void Invalid(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }
    }
}
