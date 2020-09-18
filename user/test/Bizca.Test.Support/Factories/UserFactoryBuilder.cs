namespace Bizca.Test.Support.Factories
{
    using Bizca.User.Domain.Factories;
    using Bizca.User.Domain.Rules;

    public sealed class UserFactoryBuilder
    {
        #region fields & ctor 

        private IBusinessUserRuleEngine _businessRuleEngine;
        private UserFactoryBuilder()
        {
            _businessRuleEngine = NSubstitute.Substitute.For<IBusinessUserRuleEngine>();
        }

        #endregion

        public UserFactory Build()
        {
            return new UserFactory(_businessRuleEngine);
        }
        public static UserFactoryBuilder Create()
        {
            return new UserFactoryBuilder();
        }
        public UserFactoryBuilder WithBusinessRuleEngine(IBusinessUserRuleEngine engine)
        {
            _businessRuleEngine = engine;
            return this;
        }
    }
}
