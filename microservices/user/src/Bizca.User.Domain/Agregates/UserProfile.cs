namespace Bizca.User.Domain.Agregates
{
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.Exceptions;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class UserProfile
    {
        public UserProfile(EconomicActivity economicActivity,
            Country birthCountry,
            DateTime? birthDate,
            Civility civility,
            string birthCity,
            string firstName,
            string lastName)
        {
            addresses = new List<Address>();
            channels = new List<Channel>();

            EconomicActivity = economicActivity;
            BirthCountry = birthCountry;
            BirthDate = birthDate;
            BirthCity = birthCity;
            FirstName = firstName;
            LastName = lastName;
            Civility = civility;
        }

        /// <summary>
        ///     Gets notification channels.
        /// </summary>
        public IReadOnlyCollection<Channel> Channels => channels.ToList();
        private readonly ICollection<Channel> channels;

        /// <summary>
        ///     Gets addresses.
        /// </summary>
        public IReadOnlyCollection<Address> Addresses => addresses;
        private readonly List<Address> addresses;

        /// <summary>
        ///     Gets user economic activity.
        /// </summary>
        public EconomicActivity EconomicActivity { get; internal set; }

        /// <summary>
        ///     Gets user birth country.
        /// </summary>
        public Country BirthCountry { get; internal set; }

        /// <summary>
        ///     Gets user bith date.
        /// </summary>
        public DateTime? BirthDate { get; internal set; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        public Civility Civility { get; internal set; }

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

        #region internal helpers

        /// <summary>
        ///     Update an active user address.
        /// </summary>
        /// <param name="active">Indicates whether an address is active.</param>
        /// <param name="street">street</param>
        /// <param name="city">city</param>
        /// <param name="zipCode">postal code</param>
        /// <param name="country">country</param>
        /// <param name="name">address name</param>
        internal void UpdateAddress(bool active, string street, string city, string zipCode, Country country, string name)
        {
            Address address = GetActiveAddress();
            address.Update(active, street, city, zipCode, country, name);
        }

        /// <summary>
        ///     Gets code confirmation by channel type.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        /// <param name="codeConfirmation">channel code confirmation.</param>
        internal ChannelConfirmation GetConfirmationByChannelType(ChannelType channelType, string codeConfirmation)
        {
            Channel channel = GetChannel(channelType);
            return channel.ChannelCodes.FirstOrDefault(x => x.CodeConfirmation == codeConfirmation)
                ?? throw new ChannelCodeConfirmationDoesNotExistException(new List<DomainFailure>
                {
                    new DomainFailure($"{codeConfirmation} does not exist.",
                            nameof(codeConfirmation))
                });
        }

        /// <summary>
        ///     Indicates whether a channel is confirmed or throw error if not.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        /// <param name="codeConfirmation">channel code confirmation.</param>
        internal bool IsChannelCodeConfirmed(ChannelType channelType, string codeConfirmation)
        {
            ChannelConfirmation channelConfirmation = GetConfirmationByChannelType(channelType, codeConfirmation);
            bool confirmed = channelConfirmation.ExpirationDate.CompareTo(DateTime.Now) > 0;
            if (!confirmed)
            {
                var failure = new DomainFailure($"{channelConfirmation.CodeConfirmation} has expired.",
                    nameof(channelConfirmation.CodeConfirmation));

                throw new ChannelCodeConfirmationHasExpiredUserException(new List<DomainFailure> { failure });
            }
            return confirmed;
        }

        /// <summary>
        ///     Update channel by type.
        /// </summary>
        /// <param name="channelValue">channel value.</param>
        /// <param name="channelType">channel type.</param>
        /// <param name="active">Indicates whether a channel is active.</param>
        /// <param name="confirmed">Indicates whether a channel is confirmed..</param>
        internal void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
        {
            Channel channel = GetChannel(channelType);
            channel.UpdateChannel(channelValue, active, confirmed);
        }

        /// <summary>
        ///     Gets channel by type.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        /// <param name="throwError">Indicates whether to throw an exception if channel is not found.</param>
        internal Channel GetChannel(ChannelType channelType, bool throwError = true)
        {
            Channel channel = channels.SingleOrDefault(x => x.ChannelType == channelType);
            if (channel is null && throwError)
            {
                var failure = new DomainFailure($"channel::{channelType.Code} requested does not exist.",
                    nameof(channelType));
                throw new ChannelDoesNotExistForUserException(new List<DomainFailure> { failure });
            }

            return channel;
        }

        /// <summary>
        ///     Gets channel by value
        /// </summary>
        /// <param name="channelValue">channel value</param>
        /// <param name="throwError"></param>
        internal Channel GetChannel(string channelValue, bool throwError = true)
        {
            Channel channel = channels.FirstOrDefault(x => x.ChannelValue.Equals(channelValue, StringComparison.OrdinalIgnoreCase));
            if (channel is null && throwError)
            {
                var failure = new DomainFailure($"channel requested for {channelValue} does not exist.",
                    nameof(channelValue));

                throw new ChannelDoesNotExistForUserException(new List<DomainFailure> { failure });
            }

            return channel;
        }

        /// <summary>
        ///     Add new user address.
        /// </summary>
        /// <param name="address"></param>
        internal void AddNewAddress(Address address)
        {
            foreach (Address addr in addresses)
            {
                addr.Update(false, addr.Street, addr.City, addr.ZipCode, addr.Country, addr.Name);
            }
            addresses.Add(address);
        }

        /// <summary>
        ///     Add new notification channel.
        /// </summary>
        /// <param name="channel">channel to add.</param>
        internal void AddChannel(Channel channel)
        {
            channels.Add(channel);
        }

        /// <summary>
        ///     Gets an active user address.
        /// </summary>
        internal Address GetActiveAddress()
        {
            return addresses.Find(x => x.Active);
        }

        #endregion
    }
}