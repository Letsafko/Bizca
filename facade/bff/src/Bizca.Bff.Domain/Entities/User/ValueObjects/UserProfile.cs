namespace Bizca.Bff.Domain.Entities.User.ValueObjects
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class UserProfile : ValueObject
    {
        public UserProfile(Civility civility,
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

        internal void SetChannelConfirmationStatus(ChannelConfirmationStatus confirmationStatus)
        {
            if (!ChannelConfirmationStatus.HasFlag(confirmationStatus))
            {
                ChannelConfirmationStatus |= confirmationStatus;
            }

        }

        internal void SetChannelActivationStatus(ChannelActivationStatus activationStatus)
        {
            if (!ChannelActivationStatus.HasFlag(activationStatus))
            {
                ChannelActivationStatus |= activationStatus;
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ChannelConfirmationStatus;
            yield return ChannelActivationStatus;
            yield return PhoneNumber;
            yield return FirstName;
            yield return LastName;
            yield return Whatsapp;
            yield return Civility;
            yield return Email;
        }
    }
}