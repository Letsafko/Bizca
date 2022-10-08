namespace Bizca.Bff.WebApi.UseCases.V10.UpdateSubscription
{
    using Application.UseCases.UpdateSubscription;
    using Domain.Entities.Subscription;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Update subscription.
    /// </summary>
    public sealed class UpdateSubscriptionPresenter : IUpdateSubscriptionOutput
    {
        /// <summary>
        ///     Update subscription view model.
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