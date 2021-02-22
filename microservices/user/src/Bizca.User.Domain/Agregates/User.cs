namespace Bizca.User.Domain.Agregates
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.Exceptions;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class User : Entity, IUser
    {
        public UserCode UserCode { get; internal set; }

        /// <summary>
        ///     Gets partner identification.
        /// </summary>
        public Partner Partner { get; internal set; }

        /// <summary>
        ///     Gets external user identification.
        /// </summary>
        public ExternalUserId ExternalUserId { get; internal set; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        public Civility Civility { get; internal set; }

        /// <summary>
        ///     Gets user birth country.
        /// </summary>
        public Country BirthCountry { get; internal set; }

        /// <summary>
        ///     Gets user economic activity.
        /// </summary>
        public EconomicActivity EconomicActivity { get; internal set; }

        /// <summary>
        ///     Gets user bith date.
        /// </summary>
        public DateTime BirthDate { get; internal set; }

        /// <summary>
        ///     Gets user birth city.
        /// </summary>
        public string BirthCity { get; internal set; }

        /// <summary>
        ///     Gets user lastname.
        /// </summary>
        public string LastName { get; internal set; }

        /// <summary>
        ///     Gets user firstname.
        /// </summary>
        public string FirstName { get; internal set; }

        private byte[] rowVersion;

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

        /// <summary>
        ///     Gets passwords.
        /// </summary>
        public IReadOnlyCollection<Password> Passwords => passwords.ToList();
        private readonly ICollection<Password> passwords = new List<Password>();

        /// <summary>
        ///     Gets addresses.
        /// </summary>
        public IReadOnlyCollection<Address> Addresses => addresses.ToList();
        private readonly ICollection<Address> addresses = new List<Address>();

        /// <summary>
        ///     Gets notification channels.
        /// </summary>
        public IReadOnlyCollection<Channel> Channels => channels.ToList();
        private readonly ICollection<Channel> channels = new List<Channel>();

        public Channel GetChannel(ChannelType channelType, bool throwError = true)
        {
            Channel channel = channels.SingleOrDefault(x => x.ChannelType == channelType);
            if (channel is null && throwError)
            {
                var failure = new DomainFailure($"channel::{channelType.Code} requested for user::{ExternalUserId} does not exist.",
                    nameof(channelType),
                    typeof(ChannelDoesNotExistForUserException));

                throw new ChannelDoesNotExistForUserException(new List<DomainFailure> { failure });
            }

            return channel;
        }
        public void AddNewPasword(string passwordHash, string securityStamp)
        {
            var newPassword = new Password(true, passwordHash, securityStamp);
            foreach (Password pwd in passwords)
            {
                pwd.Update(false);
            }
            passwords.Add(newPassword);
        }
        public void BuildPassword(bool active, string passwordHash, string securityStamp)
        {
            var password = new Password(active, passwordHash, securityStamp);
            passwords.Add(password);
        }
        public void AddChannel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            var channel = new Channel(value, channelType, active, confirmed);
            channels.Add(channel);
        }
        public void AddNewAddress(string street, string city, string zipCode, Country country, string name)
        {
            if(country is null && string.IsNullOrWhiteSpace(city))
            {
                return;
            }

            var address = new Address(0, true, street, city, zipCode, country, name);
            foreach (Address addr in addresses)
            {
                UpdateAddress(addr, false, addr.Street, addr.City, addr.ZipCode, addr.Country, addr.Name);
            }
            addresses.Add(address);
        }
        public void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
        {
            Channel channel = GetChannel(channelType);
            channel.UpdateChannel(channelValue, active, confirmed);
        }
        public void AddNewChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation = null)
        {
            Channel channel = GetChannel(channelType);
            if (channelConfirmation is null)
            {
                string randomCode = ChannelCodeConfirmationGenerator.GetCodeConfirmation(Partner.Settings.ChannelCodeConfirmationLength);
                DateTime expirationDate = DateTime.UtcNow.AddMinutes(Partner.Settings.ChannelCodeConfirmationExpirationDelay);
                channelConfirmation = new ChannelConfirmation(randomCode, expirationDate);
            }
            channel.AddCodeConfirmation(channelConfirmation);
        }
        public void BuildAddress(int id, bool active, string street, string city, string zipCode, Country country, string name)
        {
            var address = new Address(id, active, street, city, zipCode, country, name);
            addresses.Add(address);
        }
        public void UpdateAddress(Address address, bool active, string street, string city, string zipCode, Country country, string name)
        {
            address.Update(active, street, city, zipCode, country, name);
        }
    }
}