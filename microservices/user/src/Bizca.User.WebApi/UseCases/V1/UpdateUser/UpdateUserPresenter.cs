namespace Bizca.User.WebApi.UseCases.V1.UpdateUser
{
    using Bizca.Core.Domain;
    using Bizca.User.Application.UseCases.UpdateUser;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     update user presenter.
    /// </summary>
    public sealed class UpdateUserPresenter : IUpdateUserOutput
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
        ///     not found.
        /// </summary>
        public void NotFound()
        {
            ViewModel = new NotFoundResult();
        }

        /// <summary>
        ///     ok result.
        /// </summary>
        /// <param name="user"></param>
        public void Ok(UpdateUserDto user)
        {
            ViewModel = new OkObjectResult(new UpdateUserResponse(user));
        }
    }
}