﻿namespace Bizca.User.Domain.UnitTest.Rules.RuleEngines
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Support.Test.Builders;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.BusinessCheck;
    using Bizca.User.Domain.UnitTest.Rules.UserMustBeUniqueByPartner;
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
            Partner partner = PartnerBuilder.Instance.Build();
            UserMustBeUniqueByPartnerBuilder userMustBeUniqueBuilder = UserMustBeUniqueByPartnerBuilder.Instance.WithUserExist(userExist);
            UserRuleEngine engine = UserRuleEngineBuilder.Instance.WithBusinessRule(userMustBeUniqueBuilder.Build()).Build();

            //act
            RuleResultCollection ruleResultCollection = await engine.CheckRulesAsync(new UserRequest { Partner = partner }).ConfigureAwait(false);

            //assert
            userMustBeUniqueBuilder.WithReceiveUserExist(1);
            Check.That(ruleResultCollection)
                 .HasSize(1).And
                 .Not.IsEmpty().And
                 .HasElementThatMatches(x => x.Sucess == !userExist && string.IsNullOrWhiteSpace(x.Failure.ErrorMessage) == !userExist).And
                 .InheritsFrom<List<RuleResult>>();
        }
    }
}