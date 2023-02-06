namespace Bizca.Bff.Application.UseCases.FreezeSubscription
{
    using Domain.Entities.Subscription;

    public interface IFreezeSubscriptionOutput
    {
        void Ok(Subscription subscription);
    }
}