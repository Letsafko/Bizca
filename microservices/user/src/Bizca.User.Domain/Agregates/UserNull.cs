namespace Bizca.User.Domain.Agregates
{
    using Core.Domain.Referential.Model;
    using Entities.Channel;
    using Entities.Channel.ValueObjects;
    using System;

    public sealed class UserNull : IUser
    {
        public static UserNull Instance { get; } = new UserNull();

        public void UpdateAddress(bool active, string street, string city, string zipCode, Country country, string name)
        {
            throw new NotImplementedException();
        }

        public void AddChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public void AddNewAddress(string street, string city, string zipCode, Country country, string name)
        {
            throw new NotImplementedException();
        }

        public void AddChannel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public bool IsChannelCodeConfirmed(ChannelType channelType, string codeConfirmation)
        {
            throw new NotImplementedException();
        }

        public Channel GetChannel(ChannelType channelType, bool throwError = true)
        {
            throw new NotImplementedException();
        }

        public void AddNewPasword(string passwordHash, string securityStamp)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(EconomicActivity economicActivity,
            Country birthCountry,
            Civility civility,
            DateTime? birthDate,
            string birthCity,
            string lastName,
            string firstName)
        {
            throw new NotImplementedException();
        }
    }
}