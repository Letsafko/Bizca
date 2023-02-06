namespace Bizca.Bff.WebApi.UseCases.V1._0.PaymentSubmitted
{
    using Bizca.Bff.Application.UseCases.PaymentSubmitted;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates subscription controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Payments")]
    public sealed class UsersController : ControllerBase
    {
        private readonly PaymentSubmittedPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(PaymentSubmittedPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Subscription payment submission.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="payment">subscription payment.</param>
        /// <remarks>/Assets/addSubscriptionPayment.md</remarks>
        [HttpPost("{externalUserId}/payment/submit")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubscriptionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> PaymentSubmittedAsync([Required] string externalUserId,
            [Required] [FromBody] PaymentSubmitted payment)
        {
            PaymentSubmittedCommand command = GetPaymentSubmittedCommand(externalUserId,
                payment.Reference,
                payment.BundleId);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private PaymentSubmittedCommand GetPaymentSubmittedCommand(string externalUserId,
            string subscriptionCode,
            string bundleId)
        {
            return new PaymentSubmittedCommand(externalUserId,
                subscriptionCode,
                bundleId);
        }
    }
}