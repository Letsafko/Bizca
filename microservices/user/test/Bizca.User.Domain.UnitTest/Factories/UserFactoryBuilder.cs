namespace Bizca.User.Domain.UnitTest.Factories
{
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates.BusinessCheck;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;

    public sealed class UserFactoryBuilder
    {
        #region fields & ctor 

        private IUserRuleEngine userRuleEngine;
        private readonly IUserRepository userRepository;
        private readonly IReferentialService referentialService;
        private UserFactoryBuilder()
        {
            userRuleEngine = NSubstitute.Substitute.For<IUserRuleEngine>();
            userRepository = NSubstitute.Substitute.For<IUserRepository>();
            referentialService = NSubstitute.Substitute.For<IReferentialService>();
        }

        #endregion

        public UserFactory Build()
        {
            return new UserFactory(userRuleEngine, userRepository, referentialService);
        }

        public static UserFactoryBuilder Instance => new UserFactoryBuilder();
        public UserFactoryBuilder WithUserRuleEngine(IUserRuleEngine engine)
        {
            userRuleEngine = engine;
            return this;
        }
    }
}