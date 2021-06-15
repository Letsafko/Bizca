namespace Bizca.Bff.WebApi.UseCases.V10.CreateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    internal sealed class CreateSubscriptionResponse : SubscriptionViewModel
    {
        public CreateSubscriptionResponse(Subscription subscription) : base(subscription)
        {
        }
    }
}
