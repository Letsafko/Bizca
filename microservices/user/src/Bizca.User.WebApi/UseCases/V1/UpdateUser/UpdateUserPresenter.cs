namespace Bizca.User.WebApi.UseCases.V1.UpdateUser
{
    using Application.UseCases.UpdateUser;
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
        ///     not found.
        /// </summary>
        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
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