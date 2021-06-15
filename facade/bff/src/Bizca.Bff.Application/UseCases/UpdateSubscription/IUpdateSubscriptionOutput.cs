namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    public interface IUpdateSubscriptionOutput
    {
        void Ok(Subscription subscription);
    }
}
