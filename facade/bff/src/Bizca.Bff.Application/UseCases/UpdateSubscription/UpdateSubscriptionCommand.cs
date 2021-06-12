namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Bizca.Core.Application.Commands;
    public sealed class UpdateSubscriptionCommand : ICommand
    {
        public UpdateSubscriptionCommand(string externalUserId,
            string subscriptionCode,
            string codeInsee,
            int procedureTypeId,
            int bundleId,
            string firstName,
            string lastName,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            SubscriptionCode = subscriptionCode;
            ProcedureTypeId = procedureTypeId;
            ExternalUserId = externalUserId;
            CodeInsee = codeInsee;
            BundleId = bundleId;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Whatsapp = whatsapp;
            Email = email;
        }

        public string SubscriptionCode { get; }
        public string ExternalUserId { get; }
        public int ProcedureTypeId { get; }
        public string CodeInsee { get; }
        public int BundleId { get; }
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}
