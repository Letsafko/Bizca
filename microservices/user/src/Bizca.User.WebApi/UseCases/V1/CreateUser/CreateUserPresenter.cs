namespace Bizca.User.WebApi.UseCases.V1.CreateUser
{
    using Bizca.Core.Domain;
    using Bizca.User.Application.UseCases.CreateUser;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Creates user presenter.
    /// </summary>
    public sealed class CreateUserPresenter : ICreateUserOutput
    {
        /// <summary>
        ///     Gets view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     invalid request.
        /// </summary>
        /// <param name="notification"></param>
        public void Invalid(Notification notification)
        {
            ViewModel = new BadRequestObjectResult(notification.Errors);
        }

        /// <summary>
        ///     ok result.
        /// </summary>
        /// <param name="user"></param>
        public void Ok(CreateUserDto user)
        {
            ViewModel = new OkObjectResult(new CreateUserResponse(user));
        }
    }
}