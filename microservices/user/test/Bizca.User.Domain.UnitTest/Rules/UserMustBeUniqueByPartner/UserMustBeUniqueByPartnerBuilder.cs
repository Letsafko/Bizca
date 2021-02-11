namespace Bizca.User.Domain.UnitTest.Rules.UserMustBeUniqueByPartner
{
    using Bizca.User.Domain.Agregates.BusinessCheck.Rules;
    using Bizca.User.Domain.Agregates.Repositories;
    using NSubstitute;

    public sealed class UserMustBeUniqueByPartnerBuilder
    {
        #region fields & ctor

        private IUserRepository _userRepository;
        private UserMustBeUniqueByPartnerBuilder()
        {
            _userRepository = Substitute.For<IUserRepository>();
        }

        #endregion

        public UserMustBeUniqueByPartner Build()
        {
            return new UserMustBeUniqueByPartner(_userRepository);
        }

        public static UserMustBeUniqueByPartnerBuilder Instance => new UserMustBeUniqueByPartnerBuilder();

        public UserMustBeUniqueByPartnerBuilder WithUserExist(bool exist)
        {
            _userRepository
                .IsExistAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(exist);

            return this;
        }

        public UserMustBeUniqueByPartnerBuilder WithReceiveUserExist(int numberOfCalls)
        {
            _userRepository
                .Received(numberOfCalls)
                .IsExistAsync(Arg.Any<int>(), Arg.Any<string>());

            return this;
        }

        public UserMustBeUniqueByPartnerBuilder WithDidNotReceiveUserExist()
        {
            _userRepository
                .DidNotReceive()
                .IsExistAsync(Arg.Any<int>(), Arg.Any<string>());

            return this;
        }

        public UserMustBeUniqueByPartnerBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }
    }
}