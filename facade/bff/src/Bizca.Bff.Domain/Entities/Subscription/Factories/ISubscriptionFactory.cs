namespace Bizca.Bff.Domain.Entities.Subscription.Factories
{
    using System.Threading.Tasks;
    public interface ISubscriptionFactory
    {
        //Task<SubscriptionDto> BuildAsync(string externalUserId);
        Task<Subscription> CreateAsync(SubscriptionRequest request);
    }
}