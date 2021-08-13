namespace Bizca.Bff.WebApi.UseCases.V10.SubscriptionActivation
{
    using Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails;
    using Bizca.Bff.Application.UseCases.SubscriptionActivation;
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
    ///     Activates or desactivates subscription controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class SubscriptionsController : ControllerBase
    {
        private readonly SubscriptionActivationPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="SubscriptionsController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public SubscriptionsController(SubscriptionActivationPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Activates or desactivates a subscription.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="reference">subscription code</param>
        /// <param name="activation">subscription activation</param>
        /// <remarks>/Assets/activateOrDesacticateSubscription.md</remarks>
        [HttpPatch("{externalUserId}/subscriptions/{reference}/activation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ModelStateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
        public async Task<IActionResult> ActivateOrDesacticateSubscriptionAsync([Required] string externalUserId,
            [Required] string reference, 
            [Required][FromBody] SubscriptionActivation activation)
        {
            var command = new SubscriptionActivationCommand(Resources.PartnerCode,
                externalUserId, 
                reference,
                activation.Freeze);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}