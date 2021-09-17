namespace Bizca.Bff.WebApi.UseCases.V10.UpsertPassword
{
    using Bizca.Bff.Application.UseCases.UpsertPassword;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.Core.Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Update user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IProcessor processor;
        private readonly UpsertPasswordPresenter presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            UpsertPasswordPresenter presenter)
        {
            this.presenter = presenter;
            this.processor = processor;
        }

        /// <summary>
        ///     Creates or updates a password.
        /// </summary>
        /// <param name="password">password to create or update.</param>
        /// <remarks>/Assets/upsertPassword.md</remarks>
        [HttpPatch("password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPasswordViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IPublicResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IPublicResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Patch))]
        public async Task<IActionResult> UpsertPassword([Required][FromBody] UpsertPassword password)
        {
            var command = new UpsertPasswordCommand(Resources.PartnerCode,
                password.Resource,
                password.Password);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}