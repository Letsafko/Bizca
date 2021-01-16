namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
    using Bizca.Core.Domain;
    using Bizca.User.Application.UseCases.ConfirmChannelCode;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Confirmation channel code presenter.
    /// </summary>
    public sealed class ConfirmChannelCodePresenter : IConfirmChannelCodeOutput
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
        public void Ok(ConfirmChannelCodeDto confirmationCodeDto)
        {
            ViewModel = new OkObjectResult(new ConfirmChannelCodeResponse(confirmationCodeDto));
        }
    }
}
