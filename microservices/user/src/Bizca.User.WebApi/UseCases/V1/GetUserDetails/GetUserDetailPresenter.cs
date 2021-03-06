namespace Bizca.User.WebApi.UseCases.V1.GetUserDetails
{
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     User detail presenter.
    /// </summary>
    public sealed class GetUserDetailPresenter : IGetUserDetailOutput
    {
        /// <summary>
        ///     Gets view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     User not found.
        /// </summary>
        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }

        /// <summary>
        ///     Retrieves user informations.
        /// </summary>
        /// <param name="userDetail"></param>
        public void Ok(GetUserDetail userDetail)
        {
            ViewModel = new OkObjectResult(new GetUserDetailResponse(userDetail));
        }
    }
}