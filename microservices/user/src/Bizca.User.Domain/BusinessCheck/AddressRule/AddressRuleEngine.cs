namespace Bizca.User.Domain.BusinessCheck.AddressRule
{
    using Contract;
    using Core.Domain.Rules;
    using Core.Domain.Rules.Configuration;
    using Entities.Address;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;

    public sealed class AddressRuleEngine : BusinessRuleEngine<AddressRequest>, IAddressRuleEngine
    {
        public AddressRuleEngine(IEnumerable<IAddressRule> addressRules, IOptions<RuleOptions> ruleOptions)
            : base(addressRules, ruleOptions)
        {
        }
    }
}