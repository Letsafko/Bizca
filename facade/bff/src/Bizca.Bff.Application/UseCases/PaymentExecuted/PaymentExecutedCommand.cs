namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    using Bizca.Core.Application.Commands;
    public sealed class PaymentExecutedCommand : ICommand
    {
        public string SubscriptionCode { get; }
        public string ExternalUserId { get; }
        public PaymentExecutedCommand(string externalUserId,
            string subscriptionCode)
        {
            SubscriptionCode = subscriptionCode;
            ExternalUserId = externalUserId;
        }
    }
}
