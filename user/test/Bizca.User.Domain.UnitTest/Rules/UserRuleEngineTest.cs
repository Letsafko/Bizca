namespace Bizca.User.Domain.UnitTest.Rules
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Rules;
    using NFluent;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class UserRuleEngineTest
    {
        [Fact]
        public void BusinessUserRuleEngine_AnyConstructorArgumentIsNull_ThrowArgumentNullException()
        {
            Check.ThatCode(() => UserRuleEngineBuilder.Instance.Build())
                 .Throws<ArgumentNullException>();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task BusinessUserRuleEngine_UserExist_ThrowDomainExceptionAsync(bool userExist)
        {
            //arrange
            UserMustBeUniqueByPartnerBuilder userMustBeUniqueBuilder = UserMustBeUniqueByPartnerBuilder.Instance.WithUserExist(userExist);
            UserRuleEngine engine = UserRuleEngineBuilder.Instance.WithBusinessRule(userMustBeUniqueBuilder.Build()).Build();

            //act
            RuleResultCollection ruleResultCollection = await engine.CheckRulesAsync(new UserRequest()).ConfigureAwait(false);

            //assert
            userMustBeUniqueBuilder.WithReceiveUserExist(1);
            Check.That(ruleResultCollection)
                 .HasSize(1).And
                 .Not.IsEmpty().And
                 .HasElementThatMatches(x => x.Sucess == !userExist && string.IsNullOrWhiteSpace(x.Message) == !userExist).And
                 .InheritsFrom<List<RuleResult>>();
        }
    }
}