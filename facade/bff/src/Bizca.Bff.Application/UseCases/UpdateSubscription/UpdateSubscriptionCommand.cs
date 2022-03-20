namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Bizca.Core.Application.Commands;
    public sealed class UpdateSubscriptionCommand : ICommand
    {
        public UpdateSubscriptionCommand(string externalUserId,
            string subscriptionCode,
            string codeInsee,
            string procedureTypeId)
        {
            SubscriptionCode = subscriptionCode;
            ProcedureTypeId = procedureTypeId;
            ExternalUserId = externalUserId;
            CodeInsee = codeInsee;
        }

        public string SubscriptionCode { get; }
        public string ExternalUserId { get; }
        public string ProcedureTypeId { get; }
        public string CodeInsee { get; }
    }
}