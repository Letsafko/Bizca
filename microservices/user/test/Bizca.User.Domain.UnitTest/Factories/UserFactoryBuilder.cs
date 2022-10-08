namespace Bizca.User.Domain.UnitTest.Factories
{
    using Agregates.BusinessCheck;
    using Agregates.Factories;
    using Agregates.Repositories;
    using Core.Domain.Referential.Services;
    using NSubstitute;

    public sealed class UserFactoryBuilder
    {
        public static UserFactoryBuilder Instance => new UserFactoryBuilder();

        public UserFactory Build()
        {
            return new UserFactory(userRuleEngine, userRepository, referentialService);
        }

        public UserFactoryBuilder WithUserRuleEngine(IUserRuleEngine engine)
        {
            userRuleEngine = engine;
            return this;
        }

        #region fields & ctor

        private IUserRuleEngine userRuleEngine;
        private readonly IUserRepository userRepository;
        private readonly IReferentialService referentialService;

        private UserFactoryBuilder()
        {
            userRuleEngine = Substitute.For<IUserRuleEngine>();
            userRepository = Substitute.For<IUserRepository>();
            referentialService = Substitute.For<IReferentialService>();
        }

        #endregion
    }
}