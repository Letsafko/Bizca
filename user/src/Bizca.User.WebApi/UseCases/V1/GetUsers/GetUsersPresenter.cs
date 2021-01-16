namespace Bizca.User.WebApi.UseCases.V1.GetUsers
{
    using Bizca.Core.Application.Abstracts.Paging;
    using Bizca.Core.Domain;
    using Bizca.User.Application.UseCases.GetUser.Common;
    using Bizca.User.Application.UseCases.GetUser.List;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     User detail presenter.
    /// </summary>
    public sealed class GetUsersPresenter : IGetUsersOutput
    {
        /// <summary>
        ///     Gets view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Sets request as invalid.
        /// </summary>
        /// <param name="notification"></param>
        public void Invalid(Notification notification)
        {
            ViewModel = new BadRequestObjectResult(notification.Errors);
        }

        /// <summary>
        ///     Retrieves user informations.
        /// </summary>
        /// <param name="pagedUsers">paged users.</param>
        public void Ok(PagedResult<GetUserDto> pagedUsers)
        {
            ViewModel = new OkObjectResult(new GetUsersResponse(pagedUsers));
        }
    }
}
