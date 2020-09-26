namespace Bizca.User.Domain.UnitTest.Rules
{
    using Bizca.Test.Support.Rules;
    using Bizca.User.Domain.Rules;
    using NFluent;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class BusinessUserRuleEngineTest
    {
        [Fact]
        public void BusinessUserRuleEngine_AnyConstructorArgumentIsNull_ThrowArgumentNullException()
        {
            //arrange
            static void action()
            {
                BusinessUserRuleEngineBuilder.Create()
                    .Build();
            }

            //act
            Exception record = Record.Exception(action);

            //assert
            Check.That(record).IsInstanceOf<ArgumentNullException>();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task BusinessUserRuleEngine_UserExist_ThrowDomainExceptionAsync(bool userExist)
        {
            //arrange
            UserMustBeUniqueForPartnerBuilder
                userMustBeUniqueBuilder = UserMustBeUniqueForPartnerBuilder.Create().WithUserExist(userExist);

            BusinessUserRuleEngine
                engine = BusinessUserRuleEngineBuilder.Create().WithBusinessRule(userMustBeUniqueBuilder.Build()).Build();

            Task func()
            {
                return engine.CheckRulesAsync(new UserRequest());
            }

            //act & assert
            Exception record = default;
            if (userExist)
            {
                record = await Record.ExceptionAsync(func).ConfigureAwait(false);

                userMustBeUniqueBuilder.WithReceiveUserExist(1);
                Check.That(record).IsInstanceOf<UserDomainException>();
                Check.That((record as UserDomainException)?.Message).Equals(nameof(UserMustBeUniqueForPartner).ToLower());
            }
            else
            {
                await func().ConfigureAwait(false);

                Check.That(record).IsNull();
                userMustBeUniqueBuilder.WithReceiveUserExist(1);
            }
        }
    }
}