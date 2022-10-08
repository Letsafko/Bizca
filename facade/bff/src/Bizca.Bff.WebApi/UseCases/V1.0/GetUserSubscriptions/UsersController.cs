namespace Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptions
{
    using Application.UseCases.GetUserSubscriptions;
    using Core.Api.Modules.Conventions;
    using Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Creates subscription controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Subscriptions")]
    public sealed class UsersController : ControllerBase
    {
        private readonly GetUserSubscriptionsPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(GetUserSubscriptionsPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Retrieve user subscriptions.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpGet("{externalUserId}/subscriptions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionCollectionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
        public async Task<IActionResult> GetUserSubscriptionAsync([Required] string externalUserId)
        {
            var query = new GetUserSubscriptionsQuery(externalUserId);
            await processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}