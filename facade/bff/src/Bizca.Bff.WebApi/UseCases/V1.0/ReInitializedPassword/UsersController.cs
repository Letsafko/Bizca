namespace Bizca.Bff.WebApi.UseCases.V10.ReInitializedPassword
{
    using Application.UseCases.ReInitializedPassword;
    using Core.Api.Modules.Conventions;
    using Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Properties;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Reinitialized password controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly ReInitializedPasswordPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(ReInitializedPasswordPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Reinitialized user password.
        /// </summary>
        /// <param name="reinitializedPassword"></param>
        /// <remarks>/Assets/reinitializedPassword.md</remarks>
        [HttpPost("password/init")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPasswordViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
        public async Task<IActionResult> ReinitializedPasswordAsync(
            [Required] [FromBody] ReInitializedPassword reinitializedPassword)
        {
            var command = new ReInitializedPasswordCommand(Resources.PartnerCode,
                reinitializedPassword.Email);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}