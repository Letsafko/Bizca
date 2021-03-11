namespace Bizca.User.Application.UseCases.GetUsersByCriteria
{
    using Bizca.Core.Domain.Country;
    using Bizca.User.Domain;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using System.Collections.Generic;

    public sealed class GetUsersBuilder
    {
        private readonly GetUsers _getUser;
        private GetUsersBuilder()
        {
            _getUser = new GetUsers
            {
                Channels = new List<Channel>()
            };
        }

        public static GetUsersBuilder Instance => new GetUsersBuilder();
        public GetUsers Build()
        {
            return _getUser;
        }

        public GetUsersBuilder WithUserId(int userId)
        {
            _getUser.UserId = userId;
            return this;
        }

        public GetUsersBuilder WithUserCode(string userCode)
        {
            _getUser.UserCode = userCode;
            return this;
        }

        public GetUsersBuilder WithExternalUserId(string externalUserId)
        {
            _getUser.ExternalUserId = externalUserId;
            return this;
        }

        public GetUsersBuilder WithEmail(string email, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(email))
                _getUser.Channels.Add(new Channel(email, ChannelType.Email, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUsersBuilder WithPhoneNumber(string phoneNumber, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                _getUser.Channels.Add(new Channel(phoneNumber, ChannelType.Sms, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUsersBuilder WithWhatsapp(string whatsapp, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(whatsapp))
                _getUser.Channels.Add(new Channel(whatsapp, ChannelType.Whatsapp, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUsersBuilder WithCivility(string civility)
        {
            _getUser.Civility = civility;
            return this;
        }

        public GetUsersBuilder WithLastName(string lastname)
        {
            _getUser.LastName = lastname;
            return this;
        }

        public GetUsersBuilder WithFirstName(string firstname)
        {
            _getUser.FirstName = firstname;
            return this;
        }

        public GetUsersBuilder WithBirthCity(string birthCity)
        {
            _getUser.BirthCity = birthCity;
            return this;
        }

        public GetUsersBuilder WithBirthDate(string birthDate)
        {
            _getUser.BirthDate = birthDate;
            return this;
        }

        public GetUsersBuilder WithBirthCountry(int? countryId, string countryCode, string description)
        {
            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                _getUser.BirthCountry = new Country(countryId.Value, countryCode, description);
            }
            return this;
        }

        public GetUsersBuilder WithEconomicActivity(string economicActivity)
        {
            _getUser.EconomicActivity = economicActivity;
            return this;
        }

        public GetUsersBuilder WithAddress(int? id,
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