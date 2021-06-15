namespace Bizca.Bff.WebApi.UseCases.V10.CreateNewUser
{
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Create new user Presenter.
    /// </summary>
    public sealed class CreateNewUserPresenter : ICreateNewUserOutput
    {
        /// <summary>
        ///     Create new user view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="newUserDto"></param>
        public void Ok(CreateNewUserDto newUserDto)
        {
            ViewModel = new CreatedResult(string.Empty, new CreateNewUserResponse(newUserDto));
        }
    }
}