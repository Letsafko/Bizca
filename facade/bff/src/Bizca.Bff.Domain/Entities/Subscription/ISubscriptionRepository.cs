namespace Bizca.Bff.Domain.Entities.Subscription
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ISubscriptionRepository
    {
        Task<bool> UpsertAsync(int userId, IEnumerable<Subscription> subscriptions);
    }
}