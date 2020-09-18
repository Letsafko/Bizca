namespace Bizca.Test.Support.Rules
{
    using Bizca.User.Domain;
    using Bizca.User.Domain.Rules;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class BusinessUserRuleEngineBuilder
    {
        private ICollection<IBusinessRule<UserRequest>> _businessRules;
        private BusinessUserRuleEngineBuilder()
        {
            _businessRules = default;
        }

        public static BusinessUserRuleEngineBuilder Create()
        {
            return new BusinessUserRuleEngineBuilder();
        }

        public BusinessUserRuleEngine Build()
        {
            return new BusinessUserRuleEngine(_businessRules);
        }

        public BusinessUserRuleEngineBuilder WithBusinessRule(
            IBusinessRule<UserRequest> rule)
        {
            if (rule is null)
                return this;

            _businessRules ??= new List<IBusinessRule<UserRequest>>();
            _businessRules.Add(rule);

            return this;
        }

        public BusinessUserRuleEngineBuilder WithBusinessRules(
            ICollection<IBusinessRule<UserRequest>> rules)
        {
            if (rules?.Any() != true)
                return this;

            _businessRules ??= new List<IBusinessRule<UserRequest>>();

            foreach (IBusinessRule<UserRequest> rule in rules)
            {
                _businessRules.Add(rule);
            }

            return this;
        }
    }
}
