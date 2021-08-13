namespace Bizca.Bff.Application.UseCases.SubscriptionActivation
{
    using Bizca.Core.Application.Commands;
    public sealed class SubscriptionActivationCommand : ICommand
    {
        public SubscriptionActivationCommand(string partnerCode, 
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
