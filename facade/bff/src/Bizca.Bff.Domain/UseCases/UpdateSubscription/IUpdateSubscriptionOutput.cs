namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Domain.Entities.Subscription;

    public interface IUpdateSubscriptionOutput
    {
        void Ok(Subscription subscription);
    }
}