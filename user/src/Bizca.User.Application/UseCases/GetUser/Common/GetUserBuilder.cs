namespace Bizca.User.Application.UseCases.GetUser.Common
{
    using Bizca.User.Domain.Agregates.Users;
    using System.Collections.Generic;

    public sealed class GetUserBuilder
    {
        private readonly GetUserDto _getUser;
        private GetUserBuilder()
        {
            _getUser = new GetUserDto
            {
                Channels = new List<ChannelDto>()
            };
        }

        public static GetUserBuilder Instance => new GetUserBuilder();
        public GetUserDto Build()
        {
            return _getUser;
        }

        public GetUserBuilder WithUserId(int userId)
        {
            _getUser.UserId = userId;
            return this;
        }

        public GetUserBuilder WithUserCode(string userCode)
        {
            _getUser.UserCode = userCode;
            return this;
        }

        public GetUserBuilder WithExternalUserId(string externalUserId)
        {
            _getUser.ExternalUserId = externalUserId;
            return this;
        }

        public GetUserBuilder WithEmail(string email, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(email))
                _getUser.Channels.Add(new ChannelDto(email, nameof(NotificationChanels.Email).ToLower(), ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUserBuilder WithPhoneNumber(string phoneNumber, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                _getUser.Channels.Add(new ChannelDto(phoneNumber, nameof(NotificationChanels.Sms).ToLower(), ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUserBuilder WithWhatsapp(string whatsapp, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(whatsapp))
                _getUser.Channels.Add(new ChannelDto(whatsapp, nameof(NotificationChanels.Whatsapp).ToLower(), ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value)));

            return this;
        }

        public GetUserBuilder WithCivility(string civility)
        {
            _getUser.Civility = civility;
            return this;
        }

        public GetUserBuilder WithLastName(string lastname)
        {
            _getUser.LastName = lastname;
            return this;
        }

        public GetUserBuilder WithFirstName(string firstname)
        {
            _getUser.FirstName = firstname;
            return this;
        }

        public GetUserBuilder WithBirthCity(string birthCity)
        {
            _getUser.BirthCity = birthCity;
            return this;
        }

        public GetUserBuilder WithBirthDate(string birthDate)
        {
            _getUser.BirthDate = birthDate;
            return this;
        }

        public GetUserBuilder WithBirthCountry(string birthCountry)
        {
            _getUser.BirthCountry = birthCountry;
            return this;
        }

        public GetUserBuilder WithEconomicActivity(string economicActivity)
        {
            _getUser.EconomicActivity = economicActivity;
            return this;
        }

        private bool ConvertToBoolean(int value)
        {
            return value == 1;
        }
    }
}
