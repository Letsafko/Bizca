namespace Bizca.User.Domain.Factories
{
    using Bizca.User.Domain.Rules;
    using System;
    using System.Threading.Tasks;

    public sealed class UserFactory : IUserFactory
    {
        #region fields & ctor

        private readonly IBusinessUserRuleEngine _businessRuleEngine;
        public UserFactory(IBusinessUserRuleEngine businessRuleEngine)
        {
            _businessRuleEngine = businessRuleEngine ?? throw new ArgumentNullException(nameof(businessRuleEngine));
        }

        #endregion

        public async Task<IUser> CreateAsync(UserRequest request)
        {
            await _businessRuleEngine.CheckRulesAsync(request).ConfigureAwait(false);
            return new Entities.User();
        }
    }
}