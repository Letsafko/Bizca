namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using Bizca.User.Domain;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using Core.Domain.Referential.Model;
    using System.Collections.Generic;

    public sealed class GetUserDetailBuilder
    {
        private readonly GetUserDetail _getUser;
        private GetUserDetailBuilder()
        {
            _getUser = new GetUserDetail
            {
                Channels = new List<Channel>()
            };
        }

        public static GetUserDetailBuilder Instance => new GetUserDetailBuilder();
        public GetUserDetail Build()
        {
            return _getUser;
        }

        public GetUserDetailBuilder WithUserId(int userId)
        {
            _getUser.UserId = userId;
            return this;
        }

        public GetUserDetailBuilder WithUserCode(string userCode)
        {
            _getUser.UserCode = userCode;
            return this;
        }

        public GetUserDetailBuilder WithExternalUserId(string externalUserId)
        {
            _getUser.ExternalUserId = externalUserId;
            return this;
        }

        public GetUserDetailBuilder WithEmail(string email, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(email))
                _getUser.Channels.Add(new Channel(email, ChannelType.Email, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUserDetailBuilder WithPhoneNumber(string phoneNumber, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                _getUser.Channels.Add(new Channel(phoneNumber, ChannelType.Sms, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUserDetailBuilder WithWhatsapp(string whatsapp, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(whatsapp))
                _getUser.Channels.Add(new Channel(whatsapp, ChannelType.Whatsapp, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUserDetailBuilder WithCivility(string civility)
        {
            _getUser.Civility = civility;
            return this;
        }

        public GetUserDetailBuilder WithLastName(string lastname)
        {
            _getUser.LastName = lastname;
            return this;
        }

        public GetUserDetailBuilder WithFirstName(string firstname)
        {
            _getUser.FirstName = firstname;
            return this;
        }

        public GetUserDetailBuilder WithBirthCity(string birthCity)
        {
            _getUser.BirthCity = birthCity;
            return this;
        }

        public GetUserDetailBuilder WithBirthDate(string birthDate)
        {
            _getUser.BirthDate = birthDate;
            return this;
        }

        public GetUserDetailBuilder WithBirthCountry(string birthCountry)
        {
            _getUser.BirthCountry = birthCountry;
            return this;
        }

        public GetUserDetailBuilder WithEconomicActivity(string economicActivity)
        {
            _getUser.EconomicActivity = economicActivity;
            return this;
        }

        public GetUserDetailBuilder WithAddress(int? id,
            bool? active,
            string street,
            string city,
            string zipCode,
            int? countryId,
            string countryCode,
            string countryName,
            string name)
        {
            if (id.HasValue)
            {
                _getUser.Address = new Address(id.Value,
                    active.Value,
                    street,
                    city,
                    zipCode,
                    new Country(countryId.Value, countryCode, countryName),
                    name);
            }
            return this;
        }

        private bool ConvertToBoolean(int value)
        {
            return value == 1;
        }
    }
}