namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Rules;
    using Core.Domain.Rules.Configuration;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;

    public sealed class UserRuleEngine : BusinessRuleEngine<UserRequest>, IUserRuleEngine
    {
        public UserRuleEngine(IEnumerable<IUserRule> userBusinessRules, IOptions<RuleOptions> ruleOptions)
            : base(userBusinessRules, ruleOptions)
        {
        }
    }
}