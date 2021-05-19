namespace Bizca.Bff.Domain.Entities.Subscription
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubscriptionRepository
    {
        Task<IEnumerable<dynamic>> GetSubscriptionsAsync(string externalUserId);
        Task<bool> UpdateAsync(Subscription subscription);
        Task<bool> AddAsync(Subscription subscription);
    }
}