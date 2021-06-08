namespace Bizca.Bff.Domain.Entities.Subscription.Factories
{
    using System.Threading.Tasks;
    public interface ISubscriptionFactory
    {
        Task<Subscription> CreateAsync(SubscriptionRequest request);
    }
}