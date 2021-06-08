namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Core.Application.Commands;
    public sealed class CreateSubscriptionCommand : ICommand
    {
        public CreateSubscriptionCommand(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }
        public string ExternalUserId { get; }
    }
}
