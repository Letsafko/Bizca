namespace Bizca.Bff.Functional.Test.Builders
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.WebApi.UseCases.V10.CreateNewUser;
    using Bogus;
    public sealed class CreateUserRequestBuilder
    {
        internal static CreateUserRequestBuilder Instance => new CreateUserRequestBuilder();
        internal CreateUser Build()
        {
            return new Faker<CreateUser>()
                .RuleFor(x => x.PhoneNumber, u => u.Phone.PhoneNumber("+33#########"))
                .RuleFor(x => x.ExternalUserId, u => u.Random.AlphaNumeric(11))
                .RuleFor(x => x.Civility, _ => ((int)Civility.Mr).ToString())
                .RuleFor(x => x.FirstName, u => u.Person.FirstName)
                .RuleFor(x => x.LastName, u => u.Person.LastName)
                .RuleFor(x => x.Email, u => u.Internet.Email())
                .Generate();
        }
    }
}