namespace Bizca.Bff.WebApi.UseCases.V10.CreateSubscription
{
    using Bizca.Bff.Application.UseCases.CreateSubscription;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

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