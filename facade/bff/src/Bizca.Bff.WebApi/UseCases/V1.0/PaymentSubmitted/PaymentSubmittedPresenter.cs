namespace Bizca.Bff.WebApi.UseCases.V10.PaymentSubmitted
{
    using Application.UseCases.PaymentSubmitted;
    using Domain.Entities.Subscription;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    /// </summary>
    public sealed class PaymentSubmittedPresenter : IPaymentSubmittedOutput
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