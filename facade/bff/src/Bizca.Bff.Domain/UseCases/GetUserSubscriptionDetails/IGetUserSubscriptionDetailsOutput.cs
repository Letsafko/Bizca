namespace Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails
{
    using Domain.Entities.Subscription;

    public interface IGetUserSubscriptionDetailsOutput
    {
        void Ok(Subscription subscription);
    }
}