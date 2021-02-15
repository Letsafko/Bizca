﻿namespace Bizca.User.Domain.Agregates
{
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using System;

    public sealed class UserBuilder
    {
        private readonly User user;
        private UserBuilder()
        {
            user = new User();
        }

        public static UserBuilder Instance => new UserBuilder();
        public User Build()
        {
            return user;
        }

        public UserBuilder WithId(int id)
        {
            user.Id = id;
            return this;
        }

        public UserBuilder WithPartner(Partner partner)
        {
            user.Partner = partner;
            return this;
        }

        public UserBuilder WithBirthCountry(Country country)
        {
            user.BirthCountry = country;
            return this;
        }

        public UserBuilder WithUserCode(UserCode userCode)
        {
            user.UserCode = userCode;
            return this;
        }

        public UserBuilder WithCivility(Civility civility)
        {
            user.Civility = civility;
            return this;
        }

        public UserBuilder WithEconomicActivity(EconomicActivity economicActivity)
        {
            user.EconomicActivity = economicActivity;
            return this;
        }

        public UserBuilder WithExternalUserId(ExternalUserId externalUserId)
        {
            user.ExternalUserId = externalUserId;
            return this;
        }

        public UserBuilder WithBirthDate(DateTime birthdate)
        {
            user.BirthDate = birthdate;
            return this;
        }

        public UserBuilder WithFisrtName(string firstname)
        {
            user.FirstName = firstname;
            return this;
        }

        public UserBuilder WithLastName(string lastname)
        {
            user.LastName = lastname;
            return this;
        }

        public UserBuilder WithBirthCity(string birthCity)
        {
            user.BirthCity = birthCity;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                user.AddChannel(email, ChannelType.Email, false, false);
            }
            return this;
        }

        public UserBuilder WithEmail(string email, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                user.AddChannel(email, ChannelType.Email, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value));
            }
            return this;
        }

        public UserBuilder WithPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                user.AddChannel(phoneNumber, ChannelType.Sms, false, false);
            }
            return this;
        }

        public UserBuilder WithPhoneNumber(string phoneNumber, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                user.AddChannel(phoneNumber, ChannelType.Sms, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value));
            }
            return this;
        }

        public UserBuilder WithWhatsapp(string whatsapp)
        {
            if (!string.IsNullOrWhiteSpace(whatsapp))
            {
                user.AddChannel(whatsapp, ChannelType.Whatsapp, false, false);
            }
            return this;
        }

        public UserBuilder WithWhatsapp(string whatsapp, int? active, int? confirmed)
        {
            if (!string.IsNullOrWhiteSpace(whatsapp))
            {
                user.AddChannel(whatsapp, ChannelType.Whatsapp, ConvertToBoolean(active.Value), ConvertToBoolean(confirmed.Value));
            }
            return this;
        }

        public UserBuilder WithRowVersion(byte[] rowVersion)
        {
            user.SetRowVersion(rowVersion);
            return this;
        }

        private bool ConvertToBoolean(int value)
        {
            return value == 1;
        }
    }
}