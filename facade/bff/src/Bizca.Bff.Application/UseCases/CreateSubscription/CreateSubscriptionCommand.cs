namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Core.Application.Commands;
    public sealed class CreateSubscriptionCommand : ICommand
    {
        public CreateSubscriptionCommand(string externalUserId,
            string codeInsee,
            int procedureTypeId,
            int bundleId,
            string firstName,
            string lastName,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            ExternalUserId = externalUserId;
            ProcedureTypeId = procedureTypeId;
            CodeInsee = codeInsee;
            BundleId = bundleId;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Whatsapp = whatsapp;
            Email = email;
        }

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
