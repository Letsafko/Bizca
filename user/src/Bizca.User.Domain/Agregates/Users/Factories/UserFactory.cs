namespace Bizca.User.Domain.Agregates.Users.Factories
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Agregates.Users.Rules;
    using System;
    using System.Threading.Tasks;

    public sealed class UserFactory : IUserFactory
    {
        #region fields & ctor

        private readonly IUserRuleEngine _userRuleEngine;
        public UserFactory(IUserRuleEngine userRuleEngine)
        {
            _userRuleEngine = userRuleEngine ?? throw new ArgumentNullException(nameof(userRuleEngine));
        }

        #endregion

        public async Task<IUser> CreateAsync(UserRequest request)
        {
           RuleResultCollection collection = await _userRuleEngine.CheckRulesAsync(request).ConfigureAwait(false);
            ManageResultChecks(collection);

           return new User(new UserCode(Guid.NewGuid()), request.ExternalUserId, request.PartnerCode);
        }

        private void ManageResultChecks(RuleResultCollection collection)
        {
            foreach(RuleResult check in collection)
            {
                if (!check.Sucess)
                    throw Activator.CreateInstance(check.ExceptionType, check.Message) as DomainException;
            }
        }
    }
}