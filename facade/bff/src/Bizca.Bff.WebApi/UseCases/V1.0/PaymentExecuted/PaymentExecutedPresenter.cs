namespace Bizca.Bff.WebApi.UseCases.V10.PaymentExecuted
{
    using Application.UseCases.PaymentExecuted;
    using Domain.Entities.Subscription;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

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