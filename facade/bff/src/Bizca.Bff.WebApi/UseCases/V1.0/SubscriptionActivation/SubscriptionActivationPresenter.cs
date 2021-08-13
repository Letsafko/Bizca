namespace Bizca.Bff.WebApi.UseCases.V10.SubscriptionActivation
{
    using Bizca.Bff.Application.UseCases.SubscriptionActivation;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Subscription activation presenter.
    /// </summary>
    public sealed class SubscriptionActivationPresenter : ISubscriptionActivationOutput
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
