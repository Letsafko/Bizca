namespace Bizca.Bff.WebApi.UseCases.V1._0.ReInitializedPassword
{
    using Bizca.Bff.Application.UseCases.ReInitializedPassword;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

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