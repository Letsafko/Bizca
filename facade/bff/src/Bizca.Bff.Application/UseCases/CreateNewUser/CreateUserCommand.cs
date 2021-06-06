namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Core.Application.Commands;

    public sealed class CreateUserCommand : ICommand
    {
        public CreateUserCommand(string externalUserId,
            string partnerCode,
            Civility civility,
            string economicActivity,
            string phoneNumber,
            string firstName,
            string lastName,
            string whatsapp,
            string email)
        {
            EconomicActivity = economicActivity;
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            Civility = civility;
            LastName = lastName;
            Whatsapp = whatsapp;
            Email = email;
        }

        public string EconomicActivity { get; }
        public string ExternalUserId { get; }
        public string PartnerCode { get; }
        public string PhoneNumber { get; }
        public Civility Civility { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}
