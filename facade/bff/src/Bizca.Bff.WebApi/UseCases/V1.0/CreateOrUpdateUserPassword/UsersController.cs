namespace Bizca.Bff.WebApi.UseCases.V10.CreateOrUpdateUserPassword
{
    using Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Authenticate user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly CreateOrUpdateUserPasswordPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(CreateOrUpdateUserPasswordPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Creates or updated user password.
        /// </summary>
        /// <param name="createOrUpdateUserPassword">create or update user password</param>
        /// <remarks>/Assets/createOrUpdateUserPassword.md</remarks>
        [HttpPost("password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfirmationCodeViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ModelStateResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Edit))]
        public async Task<IActionResult> CreateOrUpdateUserPasswordAsync([Required][FromBody] CreateOrUpdateUserPassword createOrUpdateUserPassword)
        {
            var command = new CreateOrUpdateUserPasswordCommand(createOrUpdateUserPassword.Password, createOrUpdateUserPassword.Resource);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}