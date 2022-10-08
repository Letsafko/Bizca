namespace Bizca.Bff.WebApi.UseCases.V10.FreezeSubscription
{
    using Application.UseCases.FreezeSubscription;
    using Domain.Entities.Subscription;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Subscription activation presenter.
    /// </summary>
    public sealed class FreezeSubscriptionPresenter : IFreezeSubscriptionOutput
    {
        /// <summary>
        ///     User subscription view model.
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