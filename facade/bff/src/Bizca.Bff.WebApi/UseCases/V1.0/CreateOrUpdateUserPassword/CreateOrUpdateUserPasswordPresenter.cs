namespace Bizca.Bff.WebApi.UseCases.V10.CreateOrUpdateUserPassword
{
    using Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Create or update user password presenter
    /// </summary>
    public sealed class CreateOrUpdateUserPasswordPresenter : ICreateOrUpdateUserPasswordOutput
    {
        /// <summary>
        ///     Authenticate user view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        public void Ok(CreateOrUpdateUserPasswordDto createOrUpdateUserPasswordDto)
        {
            ViewModel = new OkObjectResult(new CreateOrUpdateUserPasswordResponse(createOrUpdateUserPasswordDto));
        }
    }
}