namespace Bizca.Bff.Domain.Entities.Subscription
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<SubscriberAvailability>> GetSubscribers(int organismId, int procedureTypeId);
        Task<bool> UpdateSubscriberAvailability(IEnumerable<SubscriberAvailability> subscribers);
        Task<bool> UpsertAsync(int userId, IEnumerable<Subscription> subscriptions);
        Task<Subscription> GetSubscriptionByCode(string subscriptionCode);
    }
}