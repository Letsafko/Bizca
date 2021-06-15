namespace Bizca.User.Domain.Entities.Address.BusinessCheck
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Entities.Address.BusinessCheck.Rules;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class AddressRuleEngine : IAddressRuleEngine
    {
        private readonly ICollection<IAddressRule> addressRules;
        public AddressRuleEngine(ICollection<IAddressRule> addressRules)
        {
            this.addressRules = addressRules;
        }

        public async Task<RuleResultCollection> CheckRulesAsync(AddressRequest request)
        {
            var collection = new RuleResultCollection();
            foreach (IAddressRule rule in addressRules)
            {
                RuleResult result = await rule.CheckAsync(request).ConfigureAwait(false);
                collection.Add(result);
            }
            return collection;
        }
    }
}