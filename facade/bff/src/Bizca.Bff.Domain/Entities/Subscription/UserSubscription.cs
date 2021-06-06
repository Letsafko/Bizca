namespace Bizca.Bff.Domain.Entities.Subscription
{
    using Bizca.Bff.Domain.Enumerations;
    public sealed class UserSubscription
    {
        public UserSubscription(string firstName,
            string lastName,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Whatsapp = whatsapp;
            Email = email;
        }

        public ChannelConfirmationStatus ChannelConfirmationStatus { get; internal set; }
        public ChannelActivationStatus ChannelActivationStatus { get; internal set; }
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Whatsapp { get; }
        public string Email { get; }

        internal void SetChannelConfirmationStatus(ChannelConfirmationStatus channelConfirmationStatus)
        {
            ChannelConfirmationStatus |= channelConfirmationStatus;
        }

        internal void SetChannelActivationStatus(ChannelActivationStatus channelActivationStatus)
        {
            ChannelActivationStatus |= channelActivationStatus;
        }
    }
}