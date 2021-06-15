namespace Bizca.Bff.WebApi.UseCases.V10.UpdateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;

    internal sealed class UpdateSubscriptionResponse : SubscriptionViewModel
    {
        public UpdateSubscriptionResponse(Subscription subscription) : base(subscription)
        {
        }
    }
}