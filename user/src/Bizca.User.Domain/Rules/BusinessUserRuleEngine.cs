namespace Bizca.User.Domain.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class BusinessUserRuleEngine : IBusinessUserRuleEngine
    {
        private readonly ICollection<IBusinessRule<UserRequest>> _businessRules;
        public BusinessUserRuleEngine(ICollection<IBusinessRule<UserRequest>> businessRules)
        {
            _businessRules = businessRules ?? throw new ArgumentNullException(nameof(businessRules));
        }

        public async Task CheckRulesAsync(UserRequest request)
        {
            foreach (IBusinessRule<UserRequest> rule in _businessRules)
            {
                bool result = await rule.CheckAsync(request).ConfigureAwait(false);

                if (!result)
                    throw new UserDomainException(rule.GetType().Name.ToLower());
            }
        }
    }

}
