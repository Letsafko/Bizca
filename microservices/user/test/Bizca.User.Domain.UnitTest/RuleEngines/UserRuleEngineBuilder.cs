namespace Bizca.User.Domain.UnitTest.Rules.RuleEngines
{
    using Bizca.User.Domain.Agregates.BusinessCheck;
    using Bizca.User.Domain.Agregates.BusinessCheck.Rules;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class UserRuleEngineBuilder
    {
        private ICollection<IUserRule> _businessRules;
        private UserRuleEngineBuilder()
        {
            _businessRules = default;
        }

        public static UserRuleEngineBuilder Instance => new UserRuleEngineBuilder();

        public UserRuleEngine Build()
        {
            return new UserRuleEngine(_businessRules);
        }

        public UserRuleEngineBuilder WithBusinessRule(IUserRule rule)
        {
            if (rule is null)
                return this;

            _businessRules ??= new List<IUserRule>();
            _businessRules.Add(rule);
            return this;
        }

        public UserRuleEngineBuilder WithBusinessRules(ICollection<IUserRule> rules)
        {
            if (rules?.Any() != true)
                return this;

            foreach (IUserRule rule in rules)
            {
                _businessRules.Add(rule);
            }
            return this;
        }
    }
}