namespace Bizca.User.Application.UseCases.CreateUser
{
    public sealed class CreateUserCommandBuilder
    {
        private readonly CreateUserCommand command;
        private CreateUserCommandBuilder()
        {
            command = new CreateUserCommand();
        }

        public static CreateUserCommandBuilder Instance => new CreateUserCommandBuilder();
        public CreateUserCommand Build()
        {
            return command;
        }

        public CreateUserCommandBuilder WithPartnerCode(string partnerCode)
        {
            command.PartnerCode = partnerCode;
            return this;
        }

        public CreateUserCommandBuilder WithExternalUserId(string externalUserId)
        {
            command.ExternalUserId = externalUserId;
            return this;
        }

        public CreateUserCommandBuilder WithCivility(string civility)
        {
            command.Civility = civility;
            return this;
        }

        public CreateUserCommandBuilder WithEconomicActivity(string economicActivity)
        {
            command.EconomicActivity = economicActivity;
            return this;
        }

        public CreateUserCommandBuilder WithBirthDate(string birthdate)
        {
            command.BirthDate = birthdate;
            return this;
        }

        public CreateUserCommandBuilder WithBirthCountry(string birthCountry)
        {
            command.BirthCountry = birthCountry;
            return this;
        }

        public CreateUserCommandBuilder WithBirthCity(string birthCity)
        {
            command.BirthCity = birthCity;
            return this;
        }

        public CreateUserCommandBuilder WithEmail(string email)
        {
            command.Email = email;
            return this;
        }

        public CreateUserCommandBuilder WithLastName(string lastName)
        {
            command.LastName = lastName;
            return this;
        }

        public CreateUserCommandBuilder WithFirstName(string firstName)
        {
            command.FirstName = firstName;
            return this;
        }

        public CreateUserCommandBuilder WithPhoneNumber(string phoneNumber)
        {
            command.PhoneNumber = phoneNumber;
            return this;
        }

        public CreateUserCommandBuilder WithWhatsapp(string whatsapp)
        {
            command.Whatsapp = whatsapp;
            return this;
        }
    }
}