namespace Bizca.Bff.Application.UseCases.PaymentSubmitted
{
    using Core.Application.Commands;

    public sealed class PaymentSubmittedCommand : ICommand
    {
        public PaymentSubmittedCommand(string externalUserId,
            string subscriptionCode,
            string bundleId)
        {
            SubscriptionCode = subscriptionCode;
            ExternalUserId = externalUserId;
            BundleId = bundleId;
        }

        public string SubscriptionCode { get; }
        public string ExternalUserId { get; }
        public string BundleId { get; }
    }
}