namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Core.Domain.Cqrs.Commands;

    public sealed class CreateSubscriptionCommand : ICommand
    {
        public CreateSubscriptionCommand(string externalUserId,
            string codeInsee,
            string procedureTypeId,
            string firstName,
            string lastName,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            ExternalUserId = externalUserId;
            ProcedureTypeId = procedureTypeId;
            CodeInsee = codeInsee;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Whatsapp = whatsapp;
            Email = email;
        }

        public string ExternalUserId { get; }
        public string ProcedureTypeId { get; }
        public string CodeInsee { get; }
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}