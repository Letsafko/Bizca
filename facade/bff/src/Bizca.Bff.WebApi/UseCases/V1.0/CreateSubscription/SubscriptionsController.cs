namespace Bizca.Bff.WebApi.UseCases.V1._0.CreateSubscription
{
    using Bizca.Bff.Application.UseCases.CreateSubscription;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
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
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="SubscriptionsController"/>
        /// </summary>
        /// <param name="processor"></param>
        public SubscriptionsController(IProcessor processor)
        {
            this.processor = processor;
        }

        /// <summary>
        ///     Creates a new subscription for an user.
        /// </summary>
        /// <param name="subscription">subscription.</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> CreateSubscriptionAsync([Required][FromBody] CreateSubscription subscription)
        {
            CreateSubscriptionCommand command = GetCreateSubscriptionCommand(subscription);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return new OkObjectResult(true);
            //return presenter.ViewModel;
        }

        private CreateSubscriptionCommand GetCreateSubscriptionCommand(CreateSubscription subscription)
        {
            return new CreateSubscriptionCommand(subscription.ExternalUserId);
        }
    }
}
