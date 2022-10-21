namespace Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode
{
    using Application.UseCases.ConfirmChannelCode;
    using Core.Api.Modules.Conventions;
    using Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Properties;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ViewModels;

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
        ///     Create an instance of <see cref="UsersController" />
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
        /// <param name="externalUserId">User identifier</param>
        /// <param name="channelType">Channel type</param>
        /// <param name="confirmChannel">Channel code confirmation</param>
        /// <remarks>/Assets/createUser.md</remarks>
        [HttpPost("{externalUserId}/channel/{channelType}/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfirmationCodeViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
        public async Task<IActionResult> ConfirmChannelCodeAsync([Required] string externalUserId,
            [Required] string channelType,
            [Required] [FromBody] ConfirmChannelCode confirmChannel)
        {
            var command = new ConfirmChannelCodeCommand(channelType,
                externalUserId,
                confirmChannel.ConfirmationCode,
                Resources.PartnerCode);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}