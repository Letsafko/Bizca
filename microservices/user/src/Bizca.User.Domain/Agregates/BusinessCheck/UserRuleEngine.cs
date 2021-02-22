namespace Bizca.User.Domain.Agregates.BusinessCheck
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.BusinessCheck.Rules;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class UserRuleEngine : IUserRuleEngine
    {
        private readonly ICollection<IUserRule> userBusinessRules;
        public UserRuleEngine(ICollection<IUserRule> userBusinessRules)
        {
            this.userBusinessRules = userBusinessRules;
        }

        public async Task<RuleResultCollection> CheckRulesAsync(UserRequest request)
        {
            var collection = new RuleResultCollection();
            foreach (IUserRule rule in userBusinessRules)
            {
                RuleResult result = await rule.CheckAsync(request).ConfigureAwait(false);
                collection.Add(result);
            }
            return collection;
        }
    }
}