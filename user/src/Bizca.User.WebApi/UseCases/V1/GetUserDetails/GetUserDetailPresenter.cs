﻿namespace Bizca.User.WebApi.UseCases.V1.GetUserDetails
{
    using Bizca.Core.Domain;
    using Bizca.User.Application.UseCases.GetUser.Common;
    using Bizca.User.Application.UseCases.GetUser.Detail;
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
        ///     Sets request as invalid.
        /// </summary>
        /// <param name="notification"></param>
        public void Invalid(Notification notification)
        {
            ViewModel = new BadRequestObjectResult(notification.Errors);
        }

        /// <summary>
        ///     User not found.
        /// </summary>
        public void NotFound()
        {
            ViewModel = new NotFoundResult();
        }

        /// <summary>
        ///     Retrieves user informations.
        /// </summary>
        /// <param name="userDetail"></param>
        public void Ok(GetUserDto userDetail)
        {
            ViewModel = new OkObjectResult(new GetUserDetailResponse(userDetail));
        }
    }
}
