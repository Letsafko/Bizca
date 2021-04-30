namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Core.Application.Commands;

    public sealed class CreateSubscriptionCommand : ICommand
    {
        public string ExternalUserId { get; set; }
    }
}
