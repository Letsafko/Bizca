namespace Bizca.Bff.WebApi.UseCases.V10.UpdateSubscription
{
    using Bizca.Bff.Application.UseCases.UpdateSubscription;
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
        ///     Updates subscription of an user.
        /// </summary>
        /// <param name="reference">subscription code identifier.</param>
        /// <param name="subscription">subscription.</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpPut("{reference}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Update))]
        public async Task<IActionResult> UpdateSubscriptionAsync([Required] string reference, [Required][FromBody] UpdateSubscription subscription)
        {
            UpdateSubscriptionCommand command = GetUpdateSubscriptionCommand(reference, subscription);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return new OkObjectResult(true);
            //return presenter.ViewModel;
        }

        private UpdateSubscriptionCommand GetUpdateSubscriptionCommand(string reference, UpdateSubscription subscription)
        {
            return new UpdateSubscriptionCommand(subscription.ExternalUserId,
                reference,
                subscription.CodeInsee,
                subscription.ProcedureTypeId,
                subscription.BundleId,
                subscription.FirstName,
                subscription.LastName,
                subscription.PhoneNumber,
                subscription.Whatsapp,
                subscription.Email);
        }
    }
}
