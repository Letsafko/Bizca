namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
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
        /// <param name="message"></param>
        public void Invalid(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
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