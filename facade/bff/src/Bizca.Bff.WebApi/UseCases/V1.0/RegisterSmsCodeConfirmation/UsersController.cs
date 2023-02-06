namespace Bizca.Bff.WebApi.UseCases.V1._0.RegisterSmsCodeConfirmation
{
    using Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates users controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly RegisterSmsCodeConfirmationPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(RegisterSmsCodeConfirmationPresenter presenter,
            IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Register a new sms code confirmation.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="smsCodeConfirmation">sms code confirmation</param>
        /// <remarks>/Assets/registerSmsCodeConfirmation.md</remarks>
        [HttpPost("{externalUserId}/channel/sms/code")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubscriptionCollectionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> RegisterSmsCodeConfirmationAsync([Required] string externalUserId,
            [Required] [FromBody] RegisterSmsCodeConfirmation smsCodeConfirmation)
        {
            var command = new RegisterSmsCodeConfirmationCommand(Resources.PartnerCode,
                externalUserId,
                smsCodeConfirmation.PhoneNumber);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}