namespace Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode
{
    using Bizca.Bff.Application.UseCases.ConfirmChannelCode;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Confirmation channel code presenter.
    /// </summary>
    public sealed class ConfirmChannelCodePresenter : IConfirmChannelCodeOutput
    {
        /// <summary>
        ///     Create new user view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        public void Ok(string channelType, string channelValue, bool confirmed)
        {
            ViewModel = new OkObjectResult(new ConfirmChannelCodeResponse(channelType, channelValue, confirmed));
        }
    }
}