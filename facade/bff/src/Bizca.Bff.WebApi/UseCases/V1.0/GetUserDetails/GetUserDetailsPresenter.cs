namespace Bizca.Bff.WebApi.UseCases.V10.GetUserDetails
{
    using Bizca.Bff.Application.UseCases.GetUserDetails;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Get user details preseneter.
    /// </summary>
    public sealed class GetUserDetailsPresenter : IGetUserDetailsOutput
    {
        /// <summary>
        ///     User details view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="detailsDto"></param>
        public void Ok(GetUserDetailsDto detailsDto)
        {
            ViewModel = new OkObjectResult(new UserViewModel(detailsDto));
        }
    }
}
