namespace Bizca.Bff.Application.UseCases.GetUserSubscriptions
{
    using Domain.Entities.Subscription;
    using System.Collections.Generic;

    public interface IGetUserSubscriptionsOutput
    {
        void Ok(IEnumerable<Subscription> subscriptions);
    }
}