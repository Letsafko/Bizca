namespace Bizca.Bff.Application.UseCases.GetUserSubscriptions
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using System.Collections.Generic;
    public interface IGetUserSubscriptionsOutput
    {
        void Ok(IEnumerable<Subscription> subscriptions);
    }
}