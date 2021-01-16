namespace Bizca.User.Domain.UnitTest.Factories
{
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.User.Domain.Agregates.Users.Factories;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using Bizca.User.Domain.Agregates.Users.Rules;

    public sealed class UserFactoryBuilder
    {
        #region fields & ctor 

        private IUserRuleEngine _userRuleEngine;
        private IUserRepository _userRepository;
        private ICountryRepository _countryRepository;
        private ICivilityRepository _civilityRepository;
        private IEconomicActivityRepository _economicActivityRepository;
        private UserFactoryBuilder()
        {
            _userRuleEngine = NSubstitute.Substitute.For<IUserRuleEngine>();
            _userRepository = NSubstitute.Substitute.For<IUserRepository>();
            _countryRepository = NSubstitute.Substitute.For<ICountryRepository>();
            _civilityRepository = NSubstitute.Substitute.For<ICivilityRepository>();
            _economicActivityRepository = NSubstitute.Substitute.For<IEconomicActivityRepository>();
        }

        #endregion

        public UserFactory Build()
        {
            return new UserFactory(_userRuleEngine, _userRepository, _countryRepository, _civilityRepository, _economicActivityRepository);
        }
        public static UserFactoryBuilder Instance => new UserFactoryBuilder();
        public UserFactoryBuilder WithUserRuleEngine(IUserRuleEngine engine)
        {
            _userRuleEngine = engine;
            return this;
        }

        public UserFactoryBuilder WithCountryRepository(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
            return this;
        }

        public UserFactoryBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }

        public UserFactoryBuilder WithCivilityRepository(ICivilityRepository civilityRepository)
        {
            _civilityRepository = civilityRepository;
            return this;
        }

        public UserFactoryBuilder WithEconomicActivityRepository(IEconomicActivityRepository economicActivityRepository)
        {
            _economicActivityRepository = economicActivityRepository;
            return this;
        }
    }
}