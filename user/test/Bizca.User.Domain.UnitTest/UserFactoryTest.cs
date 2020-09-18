namespace Bizca.User.Domain.UnitTest
{
    using Bizca.Test.Support.Factories;
    using Bizca.Test.Support.Rules;
    using Bizca.User.Domain.Rules;
    using NFluent;
    using System;
    using System.Threading.Tasks;
    using Xunit;
    public class UserFactoryTest
    {
        [Fact]
        public void UserFactory_OneOfConstructorAgumentIsNull_ThrowArgumentNullException()
        {
            //arrange
            static void action()
            {
                UserFactoryBuilder.Create()
                    .WithBusinessRuleEngine(default)
                    .Build();
            }

            //act
            Exception record = Record.Exception(action);

            //assert
            Check.That(record).IsInstanceOf<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UserFactory_UserAlreadyExistOrNot(bool userExist)
        {
            //arrange
            UserMustBeUniqueForPartnerBuilder
                userMustBeUniqueBuilder = UserMustBeUniqueForPartnerBuilder.Create().WithUserExist(userExist);

            BusinessUserRuleEngine
                engine = BusinessUserRuleEngineBuilder.Create().WithBusinessRule(userMustBeUniqueBuilder.Build()).Build();

            UserFactoryBuilder factoryBuilder = UserFactoryBuilder.Create().WithBusinessRuleEngine(engine);
            Factories.UserFactory factory = factoryBuilder.Build();

            //assert
            Exception record = default;
            if (userExist)
            {
                record = await Record.ExceptionAsync(() => factory.CreateAsync(new UserRequest())).ConfigureAwait(false);

                userMustBeUniqueBuilder.WithReceiveUserExist(1);
                Check.That(record).IsInstanceOf<UserDomainException>();
                Check.That((record as UserDomainException)?.Message).Equals(nameof(UserMustBeUniqueForPartner).ToLower());
            }
            else
            {
                IUser result = await factory.CreateAsync(new UserRequest()).ConfigureAwait(false);

                Check.That(result).IsNotNull();
                userMustBeUniqueBuilder.WithReceiveUserExist(1);
                Check.That(result).IsInstanceOf<Entities.User>();
            }
        }
    }
}