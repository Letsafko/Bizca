namespace Bizca.Bff.WebApi.UseCases.V10.UpsertPassword
{
    using Bizca.Bff.Application.UseCases.UpsertPassword;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Register or update user password presenter.
    /// </summary>
    public sealed class UpsertPasswordPresenter : IUpsertPasswordOutput
    {
        /// <summary>
        ///     Create user password view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="password">user password dto</param>
        public void Ok(UpsertPasswordDto password)
        {
            ViewModel = new OkObjectResult(new PasswordViewModel(password.Success));
        }
    }
}
