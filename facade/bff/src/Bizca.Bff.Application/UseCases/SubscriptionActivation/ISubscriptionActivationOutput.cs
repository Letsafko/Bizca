namespace Bizca.Bff.Application.UseCases.SubscriptionActivation
{
    using Bizca.Bff.Domain.Entities.Subscription;
    public interface ISubscriptionActivationOutput
    {
        void Ok(Subscription subscription);
    }
}
