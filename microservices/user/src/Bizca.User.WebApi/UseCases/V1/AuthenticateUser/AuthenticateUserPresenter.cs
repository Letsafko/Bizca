namespace Bizca.User.WebApi.UseCases.V1.AuthenticateUser
{
    using Bizca.User.Application.UseCases.AuthenticateUser;
    using Microsoft.AspNetCore.Mvc;
    using User = Domain.Agregates.User;

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
        /// <param name="user"></param>
        public void Ok(User user)
        {
            ViewModel = new OkObjectResult(new AuthenticateUserResponse(user));
        }

        /// <summary>
        ///     Invalid request.
        /// </summary>
        /// <param name="message"></param>
        public void Invalid(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }

        /// <summary>
        ///     Not found message.
        /// </summary>
        /// <param name="message"></param>
        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }
    }
}
