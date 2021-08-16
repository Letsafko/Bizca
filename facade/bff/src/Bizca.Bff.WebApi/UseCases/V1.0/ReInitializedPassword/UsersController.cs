namespace Bizca.Bff.WebApi.UseCases.V10.ReInitializedPassword
{
    using Bizca.Bff.Application.UseCases.ReInitializedPassword;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Reinitialized password controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(ReInitializedPasswordPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        private readonly ReInitializedPasswordPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Reinitialized user password.
        /// </summary>
        /// <param name="reinitializedPassword"></param>
        /// <remarks>/Assets/reinitializedPassword.md</remarks>
        [HttpPost("password/init")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPasswordViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ModelStateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
        public async Task<IActionResult> ReinitializedPasswordAsync([Required][FromBody] ReInitializedPassword reinitializedPassword)
        {
            var command = new ReInitializedPasswordCommand(Resources.PartnerCode,
                reinitializedPassword.Email);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}