namespace Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode
{
    using Application.UseCases.RegisterCodeConfirmation;
    using Core.Domain;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Display channel code confirmation.
    /// </summary>
    public sealed class RegisterCodeConfirmationPresenter : IRegisterCodeConfirmationOutput
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
        /// <param name="confirmationCodeDto"></param>
        public void Ok(RegisterCodeConfirmationDto confirmationCodeDto)
        {
            ViewModel = new OkObjectResult(new RegisterCodeConfirmationResponse(confirmationCodeDto));
        }

        /// <summary>
        ///     invalid request.
        /// </summary>
        /// <param name="notification"></param>
        public void Invalid(Notification notification)
        {
            ViewModel = new BadRequestObjectResult(notification.Errors);
        }
    }
}