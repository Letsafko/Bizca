namespace Bizca.Bff.WebApi.UseCases.V10.GetUsers
{
    using Bizca.Bff.Application.UseCases.GetUsers;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Get users by criteria presenter.
    /// </summary>
    public sealed class GetUsersPresenter : IGetUsersOutput
    {
        /// <summary>
        ///     Get users by criteria view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="pagedUsers"></param>
        public void Ok(GetPagedUsersDto pagedUsers)
        {
            if(!(pagedUsers is null))
            {
                ViewModel = new OkObjectResult(new UserPaginationViewModel(pagedUsers));
            }
        }
    }
}
