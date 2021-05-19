namespace Bizca.Bff.Domain.Entities.User
{
    using Bizca.Bff.Domain.Enumerations;
    public sealed class UserDto
    {
        public UserDto(Civility civility,
            string firstName,
            string lastName,
            string phoneNumber,
            string whatsapp,
            string email,
            ChannelConfirmationStatus channelConfirmationStatus,
            ChannelActivationStatus channelActivationStatus)
        {
            ChannelConfirmationStatus = channelConfirmationStatus;
            ChannelActivationStatus = channelActivationStatus;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Civility = civility;
            Whatsapp = whatsapp;
            Email = email;
        }

        public ChannelConfirmationStatus ChannelConfirmationStatus { get; internal set; }
        public ChannelActivationStatus ChannelActivationStatus { get; internal set; }
        public string PhoneNumber { get; }
        public Civility Civility { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}