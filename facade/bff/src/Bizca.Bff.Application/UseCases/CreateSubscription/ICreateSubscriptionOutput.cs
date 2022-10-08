namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Domain.Entities.Subscription;

    public interface ICreateSubscriptionOutput
    {
        void Ok(Subscription subscription);
    }
}