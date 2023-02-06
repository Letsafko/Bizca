namespace Bizca.User.Domain.UnitTest.Factories
{
    using Agregates;
    using Agregates.BusinessCheck.Exceptions;
    using BusinessCheck.UserRule;
    using Core.Domain.Referential.Model;
    using Core.Test.Support;
    using NFluent;
    using RuleEngines;
    using Rules.UserMustBeUniqueByPartner;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class UserFactoryTest
    {
        [Fact]
        public void UserFactory_OneOfConstructorAgumentIsNull_ThrowArgumentNullException()
        {
            Check.ThatCode(() =>
                {
                    return UserFactoryBuilder.Instance
                        .WithUserRuleEngine(default)
                        .Build();
                })
                .Throws<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UserFactory_UserAlreadyExistOrNot(bool userExist)
        {
            //arrange
            Partner partner = PartnerBuilder.Instance.Build();
            var request = new UserRequest { Partner = partner, ExternalUserId = "test" };
            UserMustBeUniqueByPartnerBuilder userMustBeUniqueBuilder =
                UserMustBeUniqueByPartnerBuilder.Instance.WithUserExist(userExist);
            UserRuleEngine engine = UserRuleEngineBuilder.Instance.WithBusinessRule(userMustBeUniqueBuilder.Build())
                .Build();
            UserFactoryBuilder factoryBuilder = UserFactoryBuilder.Instance.WithUserRuleEngine(engine);

            //act & assert
            if (userExist)
            {
                Check.ThatAsyncCode(() => factoryBuilder.Build().CreateAsync(request))
                    .Throws<UserAlreadyExistException>()
                    .WithMessage(
                        $"user::{request.ExternalUserId} for partner::{request.Partner.PartnerCode} must be unique.");
            }
            else
            {
                IUser response = await factoryBuilder.Build().CreateAsync(request).ConfigureAwait(false);
                Check.That(response).IsNotNull().And.InheritsFrom<IUser>();
                Check.That((response as User)?.UserIdentifier.ExternalUserId).Equals(request.ExternalUserId);
                Check.That((response as User)?.UserIdentifier.Partner).Equals(request.Partner);
            }

            userMustBeUniqueBuilder.WithReceiveUserExist(1);
        }
    }
}