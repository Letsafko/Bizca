namespace Bizca.Bff.WebApi.UseCases.V1._0.PaymentExecuted
{
    using Bizca.Bff.Application.UseCases.PaymentExecuted;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// </summary>
    public sealed class PaymentExecutedPresenter : IPaymentExecutedOutput
    {
        /// <summary>
        ///     Subscription view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="subscription"></param>
        public void Ok(Subscription subscription)
        {
            ViewModel = new OkObjectResult(new SubscriptionViewModel(subscription));
        }
    }
}