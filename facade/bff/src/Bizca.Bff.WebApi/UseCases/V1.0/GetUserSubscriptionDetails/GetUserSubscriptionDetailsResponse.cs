namespace Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptionDetails
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.WebApi.ViewModels;
    internal sealed class GetUserSubscriptionDetailsResponse : SubscriptionViewModel
    {
        public GetUserSubscriptionDetailsResponse(Subscription subscription) : base(subscription)
        {
        }
    }
}