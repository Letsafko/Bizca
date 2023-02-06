namespace Bizca.Bff.WebApi.UseCases.V1._0.UpdateSubscription
{
    using Bizca.Bff.Application.UseCases.UpdateSubscription;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

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