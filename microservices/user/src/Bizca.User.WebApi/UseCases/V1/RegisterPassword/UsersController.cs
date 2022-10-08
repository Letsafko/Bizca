namespace Bizca.User.WebApi.UseCases.V1.RegisterPassword
{
    using Application.UseCases.RegisterPassword;
    using Core.Api.Modules.Conventions;
    using Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Update user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly RegisterPasswordPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            RegisterPasswordPresenter presenter)
        {
            this.presenter = presenter;
            this.processor = processor;
        }

        /// <summary>
        ///     Register a new password.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="registerPassword">register password input.</param>
        /// <remarks>/Assets/registerPassword.md</remarks>
        [HttpPost("password")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterPasswordResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> UpdateUser([Required] string partnerCode,
            [Required] [FromBody] RegisterPassword registerPassword)
        {
            var command =
                new RegisterPasswordCommand(partnerCode, registerPassword.Resource, registerPassword.Password);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}