namespace Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode
{
    using Bizca.Core.Domain;
    using Bizca.User.Application.UseCases.RegisterCodeConfirmation;
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
    }
}