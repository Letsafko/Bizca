namespace Bizca.User.Domain.Agregates.Users.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Rules;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class UserRuleEngine : IUserRuleEngine
    {
        private readonly ICollection<IUserRule> _businessRules;
        public UserRuleEngine(ICollection<IUserRule> businessRules)
        {
            _businessRules = businessRules ?? throw new ArgumentNullException(nameof(businessRules));
        }

        public async Task<RuleResultCollection> CheckRulesAsync(UserRequest request)
        {
            var collection = new RuleResultCollection();
            foreach (IBusinessRule<UserRequest> rule in _businessRules)
            {
                RuleResult result = await rule.CheckAsync(request).ConfigureAwait(false);
                collection.Add(result);
            }
            return collection;
        }
    }
}