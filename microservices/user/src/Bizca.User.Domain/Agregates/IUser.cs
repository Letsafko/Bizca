namespace Bizca.User.Domain.Agregates
{
    using Bizca.Core.Domain.Country;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;

    public interface IUser
    {
        /// <summary>
        ///     Gets user code.
        /// </summary>
        public UserCode UserCode { get; }

        /// <summary>
        ///     Gets channel by channel type.
        /// </summary>
        /// <param name="channelType">channel type.</param>
        /// <param name="throwError">Indicates whether an exception should be thrown</param>
        Channel GetChannel(ChannelType channelType, bool throwError = true);

        /// <summary>
        ///     Add new password.
        /// </summary>
        /// <param name="passwordHash">password hash.</param>
        /// <param name="securityStamp">security stamp.</param>
        void AddNewPasword(string passwordHash, string securityStamp);

        /// <summary>
        ///     Buil a password.
        /// </summary>
        /// <param name="active">Indicates whether a password is active.</param>
        /// <param name="passwordHash">password hash.</param>
        /// <param name="securityStamp">security stamp.</param>
        void BuildPassword(bool active, string passwordHash, string securityStamp);

        /// <summary>
        ///     Add a new channel.
        /// </summary>
        /// <param name="value">channel value.</param>
        /// <param name="channelType">channel type.</param>
        /// <param name="active">Indicates whether a channel is active.</param>
        /// <param name="confirmed">Indicates wheter a channel is confirmed.</param>
        void AddChannel(string value, ChannelType channelType, bool active, bool confirmed);

        /// <summary>
        ///     Add a new code confirmation according to channel type.
        /// </summary>
        /// <param name="channelType"></param>
        /// <param name="channelConfirmation">channel code confirmation</param>
        void AddNewChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation);

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
        ///     Build an address.
        /// </summary>
        /// <param name="id">address identifier</param>
        /// <param name="active">Indicates whether an address is active.</param>
        /// <param name="street">street</param>
        /// <param name="city">city</param>
        /// <param name="zipCode">postal code</param>
        /// <param name="country">country</param>
        /// <param name="name">address name</param>
        void BuildAddress(int id, bool active, string street, string city, string zipCode, Country country, string name);

        /// <summary>
        ///     Update an address.
        /// </summary>
        /// <param name="address">address to update.</param>
        /// <param name="active">Indicates whether an address is active.</param>
        /// <param name="street">street</param>
        /// <param name="city">city</param>
        /// <param name="zipCode">postal code</param>
        /// <param name="country">country</param>
        /// <param name="name">address name</param>
        void UpdateAddress(Address address, bool active, string street, string city, string zipCode, Country country, string name);
    }
}