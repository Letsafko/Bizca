namespace Bizca.Bff.WebApi.UseCases.V10.CreateSubscription
{
    using Application.UseCases.CreateSubscription;
    using Domain.Entities.Subscription;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Create new subscription presenter.
    /// </summary>
    public sealed class CreateSubscriptionPresenter : ICreateSubscriptionOutput
    {
        /// <summary>
        ///     Create new subscription view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="subscription"></param>
        public void Ok(Subscription subscription)
        {
            ViewModel = new CreatedResult(string.Empty, new SubscriptionViewModel(subscription));
        }
    }
}