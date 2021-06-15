namespace Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails
{
    using Bizca.Bff.Domain.Entities.Subscription;
    public interface IGetUserSubscriptionDetailsOutput
    {
        void Ok(Subscription subscription);
    }
}