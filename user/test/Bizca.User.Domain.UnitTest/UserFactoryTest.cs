namespace Bizca.User.Domain.UnitTest
{
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Exceptions;
    using Bizca.User.Domain.Agregates.Users.Rules;
    using Bizca.User.Domain.UnitTest.Rules;
    using NFluent;
    using System;
    using System.Threading.Tasks;
    using Xunit;
    public class UserFactoryTest
    {
        [Fact]
        public void UserFactory_OneOfConstructorAgumentIsNull_ThrowArgumentNullException()
        {
            Check.ThatCode(() => UserFactoryBuilder.Instance.WithBusinessRuleEngine(default).Build())
                 .Throws<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UserFactory_UserAlreadyExistOrNot(bool userExist)
        {
            //arrange
            var request = new UserRequest { PartnerCode = "bizca", ExternalUserId = "test" };
            UserMustBeUniqueByPartnerBuilder userMustBeUniqueBuilder = UserMustBeUniqueByPartnerBuilder.Instance.WithUserExist(userExist);
            UserRuleEngine engine = UserRuleEngineBuilder.Instance.WithBusinessRule(userMustBeUniqueBuilder.Build()).Build();
            UserFactoryBuilder factoryBuilder = UserFactoryBuilder.Instance.WithBusinessRuleEngine(engine);

            //act & assert
            if (userExist)
            {
                Check.ThatAsyncCode(() => factoryBuilder.Build().CreateAsync(request))
                     .Throws<UserAlreadyExistException>()
                     .WithMessage($"user::{request.ExternalUserId} for partner::{request.PartnerCode} must be unique.");
            }
            else
            {
                var result = await factoryBuilder.Build().CreateAsync(request).ConfigureAwait(false) as User;
                Check.That(result).IsNotNull().And.InheritsFrom<IUser>();
                Check.That(result.ExternalUserId).Equals(request.ExternalUserId);
                Check.That(result.PartnerCode).Equals(request.PartnerCode);
            }
            userMustBeUniqueBuilder.WithReceiveUserExist(1);
        }
    }
}