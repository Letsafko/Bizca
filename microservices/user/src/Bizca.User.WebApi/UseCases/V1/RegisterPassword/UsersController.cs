namespace Bizca.User.WebApi.UseCases.V1.RegisterPassword
{
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.User.Application.UseCases.RegisterPassword;
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
        private readonly IProcessor processor;
        private readonly RegisterPasswordPresenter presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
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
        /// <param name="externalUserId"> user identifier of partner.</param>
        /// <param name="input">register password input.</param>
        /// <remarks>/Assets/registerPassword.md</remarks>
        [HttpPost("password")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterPasswordResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> UpdateUser([Required] string partnerCode, [Required][FromBody] RegisterPassword input)
        {
            var command = new RegisterPasswordCommand(partnerCode, input.ChannelResource, input.Password);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}