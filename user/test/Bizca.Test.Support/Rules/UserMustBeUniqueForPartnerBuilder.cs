namespace Bizca.Test.Support.Rules
{
    using Bizca.User.Domain.Repositories;
    using Bizca.User.Domain.Rules;
    using NSubstitute;
    using System;
    using System.Linq.Expressions;

    public sealed class UserMustBeUniqueForPartnerBuilder
    {
        #region fields & ctor

        private IUserRepository _userRepository;
        private UserMustBeUniqueForPartnerBuilder()
        {
            _userRepository = Substitute.For<IUserRepository>();
        }

        #endregion

        public UserMustBeUniqueForPartner Build()
        {
            return new UserMustBeUniqueForPartner(_userRepository);
        }
        public static UserMustBeUniqueForPartnerBuilder Create()
        {
            return new UserMustBeUniqueForPartnerBuilder();
        }

        public UserMustBeUniqueForPartnerBuilder WithUserExist(bool exist,
            Expression<Predicate<int>> expr1 = null,
            Expression<Predicate<string>> expr2 = null)
        {
            _userRepository
                .IsExistAsync(
                    expr1 != null ? Arg.Is(expr1) : Arg.Any<int>(),
                    expr2 != null ? Arg.Is(expr2) : Arg.Any<string>())
                .Returns(exist);

            return this;
        }

        public UserMustBeUniqueForPartnerBuilder WithReceiveUserExist(int numberOfCalls,
            Expression<Predicate<int>> expr1 = null,
            Expression<Predicate<string>> expr2 = null)
        {
            _userRepository
                .Received(numberOfCalls)
                .IsExistAsync(
                    expr1 != null ? Arg.Is(expr1) : Arg.Any<int>(),
                    expr2 != null ? Arg.Is(expr2) : Arg.Any<string>());

            return this;
        }

        public UserMustBeUniqueForPartnerBuilder WithDidNotReceiveUserExist(
            Expression<Predicate<int>> expr1 = null,
            Expression<Predicate<string>> expr2 = null)
        {
            _userRepository
                .DidNotReceive()
                .IsExistAsync(
                    expr1 != null ? Arg.Is(expr1) : Arg.Any<int>(),
                    expr2 != null ? Arg.Is(expr2) : Arg.Any<string>());

            return this;
        }

        public UserMustBeUniqueForPartnerBuilder WithUserRepository(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }
    }
}
