namespace Bizca.User.WebApi.UseCases.V1.ActivateUser
{
    using Bizca.User.Application.UseCases.ActivateUser;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Activate user.
    /// </summary>
    public sealed class ActivateUserPresenter : IActivateUserOutput
    {
        /// <summary>
        ///     Gets view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     User not found.
        /// </summary>
        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }

        /// <summary>
        ///     At least one of channels must be confirmed.
        /// </summary>
        /// <param name="message"></param>
        public void Invalid(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }
    }
}
