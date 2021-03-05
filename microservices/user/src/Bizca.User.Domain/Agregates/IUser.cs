namespace Bizca.User.Domain.Agregates
{
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;

    public interface IUser
    {
        /// <summary>
        ///     Update an address.
        /// </summary>
        /// <param name="active">Indicates whether an address is active.</param>
        /// <param name="street">street</param>
        /// <param name="city">city</param>
        /// <param name="zipCode">postal code</param>
        /// <param name="country">country</param>
        /// <param name="name">address name</param>
        void UpdateAddress(bool active, string street, string city, string zipCode, Country country, string name);

        /// <summary>
        ///     Add a new code confirmation according to channel type.
        /// </summary>
        /// <param name="channelType"></param>
        /// <param name="channelConfirmation">channel code confirmation</param>
        void AddChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation = null);

        /// <summary>
        ///     Update channel.
        /// </summary>
        /// <param name="channelValue">channel value.</param>
        /// <param name="channelType">channel type</param>
        /// <param name="active">Indicates whether a channel is active.</param>
        /// <param name="confirmed">Indicates whether a channel is confirmed.</param>
        void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed);

        /// <summary>
        ///     Add a new address.
        /// </summary>
        /// <param name="street">street</param>
        /// <param name="city">city</param>
        /// <param name="zipCode">postal code</param>
        /// <param name="country">country</param>
        /// <param name="name">address name</param>
        void AddNewAddress(string street, string city, string zipCode, Country country, string name);

        /// <summary>
        ///     Add a new channel.
        /// </summary>
        /// <param name="value">channel value.</param>
        /// <param name="channelType">channel type.</param>
        /// <param name="active">Indicates whether a channel is active.</param>
        /// <param name="confirmed">Indicates wheter a channel is confirmed.</param>
        void AddChannel(string value, ChannelType channelType, bool active, bool confirmed);

        /// <summary>
        ///     Indicates whether a channel is confirmed or throw error if not.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        /// <param name="codeConfirmation">channel code confirmation.</param>
        bool IsChannelCodeConfirmed(ChannelType channelType, string codeConfirmation);

        /// <summary>
        ///     Get channel by type.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        /// <param name="throwError">Indicates whether to throw error if channel does not exist.</param>
        /// <returns></returns>
        Channel GetChannel(ChannelType channelType, bool throwError = true);

        /// <summary>
        ///     Add new password.
        /// </summary>
        /// <param name="passwordHash">password hash.</param>
        /// <param name="securityStamp">security stamp.</param>
        void AddNewPasword(string passwordHash, string securityStamp);

        /// <summary>
        ///     Update user profile.
        /// </summary>
        /// <param name="economicActivity">economic activity</param>
        /// <param name="birthCountry">birth country</param>
        /// <param name="civility">civility</param>
        /// <param name="birthDate">birth date</param>
        /// <param name="birthCity">birth city</param>
        /// <param name="lastName">last name</param>
        /// <param name="firstName">first name</param>
        void UpdateProfile(EconomicActivity economicActivity,
            Country birthCountry,
            Civility civility,
            DateTime? birthDate,
            string birthCity,
            string lastName,
            string firstName);
    }
}