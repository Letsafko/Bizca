namespace Bizca.Bff.Application.UseCases.FreezeSubscription
{
    using Core.Domain.Cqrs.Commands;

    public sealed class FreezeSubscriptionCommand : ICommand
    {
        public FreezeSubscriptionCommand(string partnerCode,
            string externalUserId,
            string subscriptionCode,
            string freeze)
        {
            SubscriptionCode = subscriptionCode;
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
            Freeze = freeze;
        }

        public string SubscriptionCode { get; }
        public string ExternalUserId { get; }
        public string PartnerCode { get; }
        public string Freeze { get; }
    }
}