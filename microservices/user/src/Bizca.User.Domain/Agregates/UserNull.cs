namespace Bizca.User.Domain.Agregates
{
    using Bizca.Core.Domain.Country;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    public sealed class UserNull : IUser
    {
        public static UserNull Instance { get; } = new UserNull();
        public UserCode UserCode => new UserCode(Guid.Empty);
        public void AddNewChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation)
        {
            throw new NotImplementedException();
        }

        public void AddChannel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Channel GetChannel(ChannelType channelType, bool throwError = true)
        {
            throw new NotImplementedException();
        }

        public void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public void AddNewAddress(bool active, string street, string city, string zipCode, Country country, string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateAddress(Address address, bool active, string street, string city, string zipCode, Country country, string name)
        {
            throw new NotImplementedException();
        }

        public void AddNewPasword(string passwordHash, string securityStamp)
        {
            throw new NotImplementedException();
        }

        public void AddNewAddress(string street, string city, string zipCode, Country country, string name)
        {
            throw new NotImplementedException();
        }

        public void BuildPassword(bool active, string passwordHash, string securityStamp)
        {
            throw new NotImplementedException();
        }

        public void BuildAddress(int id, bool active, string street, string city, string zipCode, Country country, string name)
        {
            throw new NotImplementedException();
        }
    }
}