namespace Bizca.Bff.WebApi.UseCases.V10.UpsertPassword
{
    using Application.UseCases.UpsertPassword;
    using Core.Api.Modules.Conventions;
    using Core.Domain;
    using Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Properties;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Update user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly UpsertPasswordPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
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
        public async Task<IActionResult> UpsertPassword([Required] [FromBody] UpsertPassword password)
        {
            var command = new UpsertPasswordCommand(Resources.PartnerCode,
                password.Resource,
                password.Password);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}