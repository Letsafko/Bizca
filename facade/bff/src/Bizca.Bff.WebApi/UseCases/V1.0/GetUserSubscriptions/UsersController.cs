namespace Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptions
{
    using Bizca.Bff.Application.UseCases.GetUserSubscriptions;
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
    [ApiExplorerSettings(GroupName = "Subscriptions")]
    public sealed class UsersController : ControllerBase
    {
        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(GetUserSubscriptionsPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        private readonly GetUserSubscriptionsPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Retrieve user subscriptions.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpGet("{externalUserId}/subscriptions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionCollectionViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> GetUserSubscriptionAsync([Required] string externalUserId)
        {
            var query = new GetUserSubscriptionsQuery(externalUserId);
            await processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}