namespace Bizca.User.Application.UseCases.UpdateUser
{
    public sealed class UpdateUserCommandBuilder
    {
        private readonly UpdateUserCommand command;

        private UpdateUserCommandBuilder()
        {
            command = new UpdateUserCommand();
        }

        public static UpdateUserCommandBuilder Instance => new UpdateUserCommandBuilder();

        public UpdateUserCommand Build()
        {
            return command;
        }

        public UpdateUserCommandBuilder WithPartnerCode(string partnerCode)
        {
            command.PartnerCode = partnerCode;
            return this;
        }

        public UpdateUserCommandBuilder WithExternalUserId(string externalUserId)
        {
            command.ExternalUserId = externalUserId;
            return this;
        }

        public UpdateUserCommandBuilder WithCivility(string civility)
        {
            command.Civility = civility;
            return this;
        }

        public UpdateUserCommandBuilder WithEconomicActivity(string economicActivity)
        {
            command.EconomicActivity = economicActivity;
            return this;
        }

        public UpdateUserCommandBuilder WithBirthDate(string birthdate)
        {
            command.BirthDate = birthdate;
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
            command.Email = email;
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