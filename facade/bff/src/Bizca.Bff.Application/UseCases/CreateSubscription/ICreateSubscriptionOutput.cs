namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    public interface ICreateSubscriptionOutput
    {
        void Ok(Subscription subscription);
    }
}