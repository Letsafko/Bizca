namespace Bizca.User.Domain.UseCases.GetUserDetail
{
    using Application.UseCases.GetUserDetail;
    using Bizca.Core.Domain.Referential.Model;
    using Domain;
    using Entities.Address;
    using Entities.Channel;
    using System.Collections.Generic;

    public sealed class GetUserDetailBuilder
    {
        private readonly GetUserDetail _getUser;

        private GetUserDetailBuilder()
        {
            _getUser = new GetUserDetail { Channels = new List<Channel>() };
        }

        public static GetUserDetailBuilder Instance => new();

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
                _getUser.Channels.Add(new Channel(email, ChannelType.Email, ConvertToBoolean(active.GetValueOrDefault()),
                    ConvertToBoolean(confirmed.GetValueOrDefault())));

            return this;
        }

        public GetUserDetailBuilder WithPhoneNumber(string phoneNumber, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                _getUser.Channels.Add(new Channel(phoneNumber, ChannelType.Sms, ConvertToBoolean(active.GetValueOrDefault()),
                    ConvertToBoolean(confirmed.GetValueOrDefault())));

            return this;
        }

        public GetUserDetailBuilder WithWhatsapp(string whatsapp, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(whatsapp))
                _getUser.Channels.Add(new Channel(whatsapp, ChannelType.Whatsapp, ConvertToBoolean(active.GetValueOrDefault()),
                    ConvertToBoolean(confirmed.GetValueOrDefault())));

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
            Country country,
            string name)
        {
            if (id.HasValue)
                _getUser.Address = new Address(id.Value,
                    active.GetValueOrDefault(),
                    street,
                    city,
                    zipCode,
                    country,
                    name);
            return this;
        }

        private static bool ConvertToBoolean(int value) => value == 1;
    }
}