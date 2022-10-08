namespace Bizca.User.WebApi.UseCases.V1.AuthenticateUser
{
    using Application.UseCases.AuthenticateUser;
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
        private readonly AuthenticateUserPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            AuthenticateUserPresenter presenter)
        {
            this.presenter = presenter;
            this.processor = processor;
        }

        /// <summary>
        ///     Authenticates an user.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="input">authenticate user input.</param>
        /// <remarks>/Assets/AuthenticateUser.md</remarks>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthenticateUserResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> UpdateUser([Required] string partnerCode,
            [Required] [FromBody] AuthenticateUser input)
        {
            var command = new AuthenticateUserCommand(partnerCode, input.Password, input.Resource);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}