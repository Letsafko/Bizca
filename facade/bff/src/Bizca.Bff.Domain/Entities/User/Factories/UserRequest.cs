namespace Bizca.Bff.Domain.Entities.User.Factories
{
    using Bizca.Bff.Domain.Enumerations;
    public sealed class UserRequest
    {
        public UserRequest(string externalUserId,
            string partnerCode, 
            string civility,
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
            Civility = (Civility)int.Parse(civility);
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