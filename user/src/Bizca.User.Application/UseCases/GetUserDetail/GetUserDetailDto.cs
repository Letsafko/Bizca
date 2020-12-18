namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using Bizca.User.Domain.Agregates.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class GetUserDetailDto
    {
        public string UserCode { get; set; }
        public string ExternalUserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Civility { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public IEnumerable<string> Channels { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string EconomicActivity { get; set; }
    }

    public sealed class GetUserDetailBuilder
    {
        private readonly GetUserDetailDto _getUserDetail;
        private GetUserDetailBuilder()
        {
            _getUserDetail = new GetUserDetailDto();
        }

        public static GetUserDetailBuilder Instance => new GetUserDetailBuilder();
        public GetUserDetailDto Build()
        {
            return _getUserDetail;
        }

        public GetUserDetailBuilder WithUserCode(string userCode)
        {
            _getUserDetail.UserCode = userCode;
            return this;
        }

        public GetUserDetailBuilder WithExternalUserId(string externalUserId)
        {
            _getUserDetail.ExternalUserId = externalUserId;
            return this;
        }

        public GetUserDetailBuilder WithEmail(string email)
        {
            _getUserDetail.Email = email;
            return this;
        }

        public GetUserDetailBuilder WithPhoneNumber(string phoneNumber)
        {
            _getUserDetail.PhoneNumber = phoneNumber;
            return this;
        }

        public GetUserDetailBuilder WithCivility(string civility)
        {
            _getUserDetail.Civility = civility;
            return this;
        }

        public GetUserDetailBuilder WithLastName(string lastname)
        {
            _getUserDetail.LastName = lastname;
            return this;
        }

        public GetUserDetailBuilder WithFirstName(string firstname)
        {
            _getUserDetail.FirstName = firstname;
            return this;
        }

        public GetUserDetailBuilder WithChannels(NotificationChanels channels)
        {
            _getUserDetail.Channels = from NotificationChanels item
                                      in Enum.GetValues(typeof(NotificationChanels)).Cast<NotificationChanels>()
                                      where (channels & item) != 0
                                      select item.ToString();
            return this;
        }

        public GetUserDetailBuilder WithBirthCity(string birthCity)
        {
            _getUserDetail.BirthCity = birthCity;
            return this;
        }

        public GetUserDetailBuilder WithBirthDate(string birthDate)
        {
            _getUserDetail.BirthDate = birthDate;
            return this;
        }

        public GetUserDetailBuilder WithBirthCountry(string birthCountry)
        {
            _getUserDetail.BirthCountry = birthCountry;
            return this;
        }

        public GetUserDetailBuilder WithEconomicActivity(string economicActivity)
        {
            _getUserDetail.EconomicActivity = economicActivity;
            return this;
        }
    }
}
