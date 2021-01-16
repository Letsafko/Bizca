namespace Bizca.User.Application.UseCases.CreateUser
{
    using System;
    using System.Text.RegularExpressions;
    public sealed class CreateUserCommandBuilder
    {
        private readonly CreateUserCommand command;
        private CreateUserCommandBuilder()
        {
            command = new CreateUserCommand();
        }

        private const string expression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public static CreateUserCommandBuilder Instance => new CreateUserCommandBuilder();
        public CreateUserCommand Build()
        {
            return command;
        }

        public CreateUserCommandBuilder WithPartnerCode(string partnerCode)
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

        public CreateUserCommandBuilder WithExternalUserId(string externalUserId)
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

        public CreateUserCommandBuilder WithCivility(string civility)
        {
            if (string.IsNullOrWhiteSpace(civility))
            {
                command.ModelState.Add(nameof(civility), $"{nameof(civility)} is required.");
            }
            else if (int.TryParse(civility, out int civilityId))
            {
                command.Civility = civilityId;
            }
            else
            {
                command.ModelState.Add(nameof(civility), $"{nameof(civility)} is invalid.");
            }
            return this;
        }

        public CreateUserCommandBuilder WithEconomicActivity(string economicActivity)
        {
            if (string.IsNullOrWhiteSpace(economicActivity))
            {
                command.ModelState.Add(nameof(economicActivity), $"{nameof(economicActivity)} is required.");
            }
            else if (int.TryParse(economicActivity, out int economicActivityId))
            {
                command.EconomicActivity = economicActivityId;
            }
            else
            {
                command.ModelState.Add(nameof(economicActivity), $"{nameof(economicActivity)} is invalid.");
            }
            return this;
        }

        public CreateUserCommandBuilder WithBirthDate(string birthdate)
        {
            if (string.IsNullOrWhiteSpace(birthdate))
            {
                command.ModelState.Add(nameof(birthdate), $"{nameof(birthdate)} is required.");
            }
            else if (DateTime.TryParse(birthdate, out DateTime birthday))
            {
                command.BirthDate = birthday;
            }
            else
            {
                command.ModelState.Add(nameof(birthdate), $"{nameof(birthdate)} is invalid.");
            }
            return this;
        }

        public CreateUserCommandBuilder WithBirthCountry(string birthCountry)
        {
            if (string.IsNullOrWhiteSpace(birthCountry))
            {
                command.ModelState.Add(nameof(birthCountry), $"{nameof(birthCountry)} is required.");
            }
            else
            {
                command.BirthCountry = birthCountry;
            }
            return this;
        }

        public CreateUserCommandBuilder WithBirthCity(string birthCity)
        {
            if (string.IsNullOrWhiteSpace(birthCity))
            {
                command.ModelState.Add(nameof(birthCity), $"{nameof(birthCity)} is required.");
            }
            else
            {
                command.BirthCity = birthCity;
            }
            return this;
        }

        public CreateUserCommandBuilder WithEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                command.ModelState.Add(nameof(email), $"{nameof(email)} is required.");
            }
            else if (!Regex.IsMatch(email, expression))
            {
                command.ModelState.Add(nameof(email), "email is invalid.");
            }
            else
            {
                command.Email = email;
            }
            return this;
        }

        public CreateUserCommandBuilder WithLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                command.ModelState.Add(nameof(lastName), $"{nameof(lastName)} is required.");
            }
            else
            {
                command.LastName = lastName;
            }
            return this;
        }

        public CreateUserCommandBuilder WithFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                command.ModelState.Add(nameof(firstName), $"{nameof(firstName)} is required.");
            }
            else
            {
                command.FirstName = firstName;
            }
            return this;
        }

        public CreateUserCommandBuilder WithPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                command.ModelState.Add(nameof(phoneNumber), $"{nameof(phoneNumber)} is required.");
            }
            else
            {
                command.PhoneNumber = phoneNumber;
            }
            return this;
        }

        public CreateUserCommandBuilder WithWhatsapp(string whatsapp)
        {
            if (string.IsNullOrWhiteSpace(whatsapp))
            {
                command.ModelState.Add(nameof(whatsapp), $"{nameof(whatsapp)} is required.");
            }
            else
            {
                command.Whatsapp = whatsapp;
            }
            return this;
        }
    }
}
