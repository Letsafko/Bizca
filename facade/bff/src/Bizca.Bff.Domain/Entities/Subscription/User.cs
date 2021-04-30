namespace Bizca.Bff.Domain.Entities.Subscription
{
    public sealed class User
    {
        public bool PhoneNumberConfirmed { get; }
        public bool WhatsappConfirmed { get; }
        public bool EmailConfirmed { get; }
        public string ExternalUserId { get; }
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Whatsapp { get; }
        public string Email { get; }
        public User(string externalUserId,
            string phoneNumber,
            string whatsapp,
            string email,
            string firstName,
            string lastName,
            bool phoneNumberConfirmed,
            bool whatsappConfirmed,
            bool emailConfirmed)
        {
            PhoneNumberConfirmed = phoneNumberConfirmed;
            WhatsappConfirmed = whatsappConfirmed;
            EmailConfirmed = emailConfirmed;
            ExternalUserId = externalUserId;
            PhoneNumber = phoneNumber;
            Whatsapp = whatsapp;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}