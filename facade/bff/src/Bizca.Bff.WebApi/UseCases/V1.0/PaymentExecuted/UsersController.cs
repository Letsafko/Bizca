namespace Bizca.Bff.WebApi.UseCases.V1._0.PaymentExecuted
{
    using Bizca.Bff.Application.UseCases.PaymentExecuted;
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
        private readonly PaymentExecutedPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(PaymentExecutedPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Subscription payment confirmation.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="payment">subscription payment.</param>
        /// <remarks>/Assets/addSubscriptionPayment.md</remarks>
        [HttpPatch("{externalUserId}/payment/confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Patch))]
        public async Task<IActionResult> AddSubscriptionPaymentAsync([Required] string externalUserId,
            [Required] [FromBody] PaymentExecuted payment)
        {
            PaymentExecutedCommand command = GetPaymentExecutedCommand(externalUserId,
                payment.SubscriptionCode);

            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private PaymentExecutedCommand GetPaymentExecutedCommand(string externalUserId,
            string subscriptionCode)
        {
            return new PaymentExecutedCommand(externalUserId,
                subscriptionCode);
        }
    }
}