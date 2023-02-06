namespace Bizca.User.Domain.UnitTest.Rules.UserMustBeUniqueByPartner
{
    using Agregates.Repositories;
    using BusinessCheck.UserRule;
    using NSubstitute;

    public sealed class UserMustBeUniqueByPartnerBuilder
    {
        public static UserMustBeUniqueByPartnerBuilder Instance => new UserMustBeUniqueByPartnerBuilder();

        public UserMustBeUniqueByPartner Build()
        {
            return new UserMustBeUniqueByPartner(_userRepository);
        }

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

        #region fields & ctor

        private IUserRepository _userRepository;

        private UserMustBeUniqueByPartnerBuilder()
        {
            _userRepository = Substitute.For<IUserRepository>();
        }

        #endregion
    }
}