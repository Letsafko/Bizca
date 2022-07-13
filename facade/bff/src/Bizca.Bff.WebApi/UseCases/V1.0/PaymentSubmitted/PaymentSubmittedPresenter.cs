namespace Bizca.Bff.WebApi.UseCases.V10.PaymentSubmitted
{
    using Bizca.Bff.Application.UseCases.PaymentSubmitted;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 
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
