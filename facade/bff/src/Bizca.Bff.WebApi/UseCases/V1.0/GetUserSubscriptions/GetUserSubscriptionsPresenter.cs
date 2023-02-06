namespace Bizca.Bff.WebApi.UseCases.V1._0.GetUserSubscriptions
{
    using Bizca.Bff.Application.UseCases.GetUserSubscriptions;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>
    ///     User subscription presenter.
    /// </summary>
    public sealed class GetUserSubscriptionsPresenter : IGetUserSubscriptionsOutput
    {
        /// <summary>
        ///     User subscription view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="subscriptions"></param>
        public void Ok(IEnumerable<Subscription> subscriptions)
        {
            ViewModel = new OkObjectResult(new SubscriptionCollectionViewModel(subscriptions));
        }
    }
}