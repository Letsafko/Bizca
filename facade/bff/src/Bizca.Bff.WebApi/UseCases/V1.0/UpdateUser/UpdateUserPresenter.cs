namespace Bizca.Bff.WebApi.UseCases.V10.UpdateUser
{
    using Bizca.Bff.Application.UseCases.UpdateUser;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    ///     Update user presenter.
    /// </summary>
    public sealed class UpdateUserPresenter : IUpdateUserOutput
    {
        /// <summary>
        ///     Updates an user view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="user"></param>
        public void Ok(UpdateUserDto user)
        {
            ViewModel = new OkObjectResult(new UserViewModel(user));
        }
    }
}