namespace Bizca.User.Application.UseCases.UpdateUser
{
    using System;
    using System.Text.RegularExpressions;

    public sealed class UpdateUserCommandBuilder
    {
        private readonly UpdateUserCommand command;
        private UpdateUserCommandBuilder()
        {
            command = new UpdateUserCommand();
        }

        private const string expression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public static UpdateUserCommandBuilder Instance => new UpdateUserCommandBuilder();
        public UpdateUserCommand Build()
        {
            return command;
        }

        public UpdateUserCommandBuilder WithPartnerCode(string partnerCode)
        {
            if (string.IsNullOrWhiteSpace(partnerCode))
            {
                command.ModelState.Add(nameof(partnerCode), $"{nameof(partnerCode)} is required.");
            }
            else
            {
                command.PartnerCode = partnerCode;
            }
            return this;
        }

        public UpdateUserCommandBuilder WithExternalUserId(string externalUserId)
        {
            if (string.IsNullOrWhiteSpace(externalUserId))
            {
                command.ModelState.Add(nameof(externalUserId), $"{nameof(externalUserId)} is required.");
            }
            else
            {
                command.ExternalUserId = externalUserId;
            }
            return this;
        }

        public UpdateUserCommandBuilder WithCivility(string civility)
        {
            if (!string.IsNullOrWhiteSpace(civility))
            {
                if (int.TryParse(civility, out int civilityId))
                {
                    command.Civility = civilityId;
                }
                else
                {
                    command.ModelState.Add(nameof(civility), $"{nameof(civility)} is invalid.");
                }
            }
            return this;
        }

        public UpdateUserCommandBuilder WithEconomicActivity(string economicActivity)
        {
            if (!string.IsNullOrWhiteSpace(economicActivity))
            {
                if (int.TryParse(economicActivity, out int economicActivityId))
                {
                    command.EconomicActivity = economicActivityId;
                }
                else
                {
                    command.ModelState.Add(nameof(economicActivity), $"{nameof(economicActivity)} is invalid.");
                }
            }
            return this;
        }

        public UpdateUserCommandBuilder WithBirthDate(string birthdate)
        {
            if (!string.IsNullOrWhiteSpace(birthdate))
            {
                if (DateTime.TryParse(birthdate, out DateTime birthday))
                {
                    command.BirthDate = birthday;
                }
                else
                {
                    command.ModelState.Add(nameof(birthdate), $"{nameof(birthdate)} is invalid.");
                }
            }
            return this;
        }

        public UpdateUserCommandBuilder WithBirthCountry(string birthCountry)
        {
            command.BirthCountry = birthCountry;
            return this;
        }

        public UpdateUserCommandBuilder WithBirthCity(string birthCity)
        {
            command.BirthCity = birthCity;
            return this;
        }

        public UpdateUserCommandBuilder WithEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!Regex.IsMatch(email, expression))
                {
                    command.ModelState.Add(nameof(email), "email is invalid.");
                }
                else
                {
                    command.Email = email;
                }
            }
            return this;
        }

        public UpdateUserCommandBuilder WithLastName(string lastName)
        {
            command.LastName = lastName;
            return this;
        }

        public UpdateUserCommandBuilder WithFirstName(string firstName)
        {
            command.FirstName = firstName;
            return this;
        }

        public UpdateUserCommandBuilder WithPhoneNumber(string phoneNumber)
        {
            command.PhoneNumber = phoneNumber;
            return this;
        }

        public UpdateUserCommandBuilder WithWhatsapp(string whatsapp)
        {
            command.Whatsapp = whatsapp;
            return this;
        }
    }
}
