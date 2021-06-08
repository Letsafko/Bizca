﻿namespace Bizca.Bff.Domain.Entities.User.ValueObjects
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
            string email)
        {
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

        #region internal helpers

        internal void SetChannelConfirmationStatus(ChannelConfirmationStatus channelConfirmationStatus)
        {
            ChannelConfirmationStatus |= channelConfirmationStatus;
        }

        internal void SetChannelActivationStatus(ChannelActivationStatus channelActivationStatus)
        {
            ChannelActivationStatus |= channelActivationStatus;
        }

        #endregion
    }
}