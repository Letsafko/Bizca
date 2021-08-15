namespace Bizca.Bff.WebApi.ViewModels
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class SubscriptionCollectionViewModel : List<SubscriptionViewModel>
    {
        public SubscriptionCollectionViewModel(IEnumerable<Subscription> subscriptions)
        {
            if (subscriptions?.Any() == true)
            {
                foreach (Subscription sub in subscriptions)
                {
                    Add(new SubscriptionViewModel(sub));
                }
            }
        }
    }
}