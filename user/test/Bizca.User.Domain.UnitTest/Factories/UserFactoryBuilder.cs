namespace Bizca.User.Domain.UnitTest.Factories
{
    using Bizca.User.Domain.Agregates.Users.Factories;
    using Bizca.User.Domain.Agregates.Users.Rules;

    public sealed class UserFactoryBuilder
    {
        #region fields & ctor 

        private IUserRuleEngine _businessRuleEngine;
        private UserFactoryBuilder()
        {
            _businessRuleEngine = NSubstitute.Substitute.For<IUserRuleEngine>();
        }

        #endregion

        public UserFactory Build()
        {
            return new UserFactory(_businessRuleEngine);
        }
        public static UserFactoryBuilder Instance => new UserFactoryBuilder();
        public UserFactoryBuilder WithBusinessRuleEngine(IUserRuleEngine engine)
        {
            _businessRuleEngine = engine;
            return this;
        }
    }
}