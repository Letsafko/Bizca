namespace Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptions
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class GetUserSubscriptionsResponse
    {
        public GetUserSubscriptionsResponse(IEnumerable<Subscription> subscriptions)
        {
            if(subscriptions?.Any() == true)
            {
                Subscriptions = new SubscriptionCollectionViewModel();
                foreach (Subscription sub in subscriptions)
                {
                     Subscriptions.Add(new SubscriptionViewModel(sub));
                }
            }
        }

        public SubscriptionCollectionViewModel Subscriptions { get; }
    }
}