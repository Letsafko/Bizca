namespace Bizca.Bff.WebApi.UseCases.V10.ReInitializedPassword
{
    using Application.UseCases.ReInitializedPassword;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Reinitialized password presenter.
    /// </summary>
    public sealed class ReInitializedPasswordPresenter : IReInitializedPasswordOutput
    {
        /// <summary>
        ///     User password view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="reInitializedPasswordDto"></param>
        public void Ok(ReInitializedPasswordDto reInitializedPasswordDto)
        {
            ViewModel = new OkObjectResult(new UserPasswordViewModel(reInitializedPasswordDto.Success));
        }
    }
}