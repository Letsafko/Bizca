namespace Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptionDetails
{
    using Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates subscription controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class SubscriptionsController : ControllerBase
    {
        private readonly GetUserSubscriptionDetailsPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="SubscriptionsController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public SubscriptionsController(GetUserSubscriptionDetailsPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Retrieve user subscription details.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="reference">subscription code</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpGet("{externalUserId}/subscriptions/{reference}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ModelStateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
        public async Task<IActionResult> GetUserSubscriptionDetailsAsync([Required] string externalUserId,
            [Required] string reference)
        {
            var query = new GetUserSubscriptionDetailsQuery(externalUserId, reference);
            await processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}