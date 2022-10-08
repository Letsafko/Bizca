﻿namespace Bizca.User.Domain.Agregates
{
    using Core.Domain.Referential.Model;
    using Entities.Address;
    using Entities.Channel;
    using Entities.Channel.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ValueObjects;

    public sealed class User : IUser
    {
        private readonly ICollection<Password> passwords;
        private byte[] rowVersion;

        /// <summary>
        ///     Creates an instance of <see cref="User" />
        /// </summary>
        /// <param name="userIdentifier">user identifier.</param>
        /// <param name="profile">user profile.</param>
        public User(UserIdentifier userIdentifier, UserProfile profile)
        {
            passwords = new List<Password>();
            UserIdentifier = userIdentifier;
            Profile = profile;
        }

        /// <summary>
        ///     Gets passwords.
        /// </summary>
        public IReadOnlyCollection<Password> Passwords => passwords.ToList();

        /// <summary>
        ///     Gets user identifier.
        /// </summary>
        public UserIdentifier UserIdentifier { get; }

        /// <summary>
        ///     Gets user profile.
        /// </summary>
        public UserProfile Profile { get; }

        /// <summary>
        ///     Gets row version.
        /// </summary>
        internal void SetRowVersion(byte[] value)
        {
            rowVersion = value;
        }

        /// <summary>
        ///     Gets row version.
        /// </summary>
        public byte[] GetRowVersion()
        {
            return rowVersion;
        }

        #region helpers

        internal void BuildAddress(int addressId, bool active, string street, string city, string zipCode,
            Country country, string name)
        {
            var address = new Address(addressId, active, street, city, zipCode, country, name);
            Profile.AddNewAddress(address);
        }

        public void UpdateAddress(bool active, string street, string city, string zipCode, Country country, string name)
        {
            Profile.UpdateAddress(active, street, city, zipCode, country, name);
        }

        public void AddChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation = null)
        {
            Channel channel = Profile.GetChannel(channelType);
            if (channelConfirmation is null)
            {
                string randomCode =
                    ChannelCodeConfirmationGenerator.GetCodeConfirmation(UserIdentifier.Partner.Settings
                        .ChannelCodeConfirmationLength);
                DateTime expirationDate =
                    DateTime.UtcNow.AddMinutes(UserIdentifier.Partner.Settings.ChannelCodeConfirmationExpirationDelay);
                channelConfirmation = new ChannelConfirmation(randomCode, expirationDate);
            }

            channel.AddNewCodeConfirmation(channelConfirmation);
        }

        public void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
        {
            Profile.UpdateChannel(channelValue, channelType, active, confirmed);
        }

        public void AddNewAddress(string street, string city, string zipCode, Country country, string name)
        {
            var address = new Address(0, true, street, city, zipCode, country, name);
            Profile.AddNewAddress(address);
        }

        public void AddChannel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            var channel = new Channel(value, channelType, active, confirmed);
            Profile.AddChannel(channel);
        }

        public bool IsChannelCodeConfirmed(ChannelType channelType, string codeConfirmation)
        {
            return Profile.IsChannelCodeConfirmed(channelType, codeConfirmation);
        }

        internal void BuildPasword(bool active, string passwordHash, string securityStamp)
        {
            var password = new Password(active, passwordHash, securityStamp);
            passwords.Add(password);
        }

        public Channel GetChannel(ChannelType channelType, bool throwError = true)
        {
            return Profile.GetChannel(channelType, throwError);
        }

        public void AddNewPasword(string passwordHash, string securityStamp)
        {
            var newPassword = new Password(true, passwordHash, securityStamp);
            foreach (Password pwd in passwords) pwd.Update(false);
            passwords.Add(newPassword);
        }

        public void UpdateProfile(EconomicActivity economicActivity,
            Country birthCountry,
            Civility civility,
            DateTime? birthDate,
            string birthCity,
            string lastName,
            string firstName)
        {
            Profile.FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : Profile.FirstName;
            Profile.BirthCity = !string.IsNullOrWhiteSpace(birthCity) ? birthCity : Profile.BirthCity;
            Profile.LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : Profile.LastName;
            Profile.EconomicActivity = economicActivity ?? Profile.EconomicActivity;
            Profile.BirthCountry = birthCountry ?? Profile.BirthCountry;
            Profile.BirthDate = birthDate ?? Profile.BirthDate;
            Profile.Civility = civility ?? Profile.Civility;
        }

        #endregion
    }
}