namespace Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode
{
    using Bizca.Bff.Application.UseCases.ConfirmChannelCode;
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
    ///     Creates user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly ConfirmChannelCodePresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(ConfirmChannelCodePresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Confirm channel code.
        /// </summary>
        /// <param name="externalUserId"></param>
        /// <param name="confirmChannel">channel code confirmation</param>
        /// <remarks>/Assets/createUser.md</remarks>
        [HttpPost("{externalUserId}/channel/code/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfirmationCodeViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> ConfirmChannelCodeAsync([Required] string externalUserId, [Required][FromBody] ConfirmChannelCode confirmChannel)
        {
            var command = new ConfirmChannelCodeCommand(confirmChannel.Channel,
                externalUserId,
                confirmChannel.ConfirmationCode,
                Resources.PartnerCode);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}