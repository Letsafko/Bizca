namespace Bizca.Bff.Application.UseCases.FreezeSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    public interface IFreezeSubscriptionOutput
    {
        void Ok(Subscription subscription);
    }
}
