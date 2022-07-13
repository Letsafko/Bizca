namespace Bizca.Bff.WebApi.UseCases.V10.FreezeSubscription
{
    using Bizca.Bff.Application.UseCases.FreezeSubscription;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Bff.WebApi.ViewModels;
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
    [ApiExplorerSettings(GroupName = "Subscriptions")]
    public sealed class UsersController : ControllerBase
    {
        private readonly FreezeSubscriptionPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(FreezeSubscriptionPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Freezing of a subscription.
        /// </summary>
        /// <param name="externalUserId">User identifier.</param>
        /// <param name="reference">Subscription reference.</param>
        /// <param name="subscriptionFreeze">Value indicates whether a subscription should be freeze or not.</param>
        /// <remarks>/Assets/activateOrDesacticateSubscription.md</remarks>
        [HttpPatch("{externalUserId}/subscriptions/{reference}/freeze")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Patch))]
        public async Task<IActionResult> FreezeSubscriptionAsync([Required] string externalUserId,
            [Required] string reference,
            [Required][FromBody] FreezeSubscription subscriptionFreeze)
        {
            var command = new FreezeSubscriptionCommand(Resources.PartnerCode,
                externalUserId,
                reference,
                subscriptionFreeze.Flag);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}