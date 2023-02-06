namespace Bizca.User.WebApi.UseCases.V1.RegisterPassword
{
    using Application.UseCases.RegisterPassword;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Register password presenter.
    /// </summary>
    public sealed class RegisterPasswordPresenter : IRegisterPasswordOutput
    {
        /// <summary>
        ///     Gets view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Password ceated.
        /// </summary>
        /// <param name="passwordCreated"></param>
        public void Ok(RegisterPasswordDto passwordCreated)
        {
            ViewModel = new CreatedResult(string.Empty, new RegisterPasswordResponse(passwordCreated));
        }

        /// <summary>
        ///     User not found.
        /// </summary>
        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }
    }
}