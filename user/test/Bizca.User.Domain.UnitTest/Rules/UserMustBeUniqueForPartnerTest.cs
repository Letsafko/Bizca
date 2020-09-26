namespace Bizca.User.Domain.UnitTest.Rules
{
    using Bizca.Test.Support.Rules;
    using NFluent;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class UserMustBeUniqueForPartnerTest
    {
        [Fact]
        public void UserMustBeUniqueForPartner_AnyConstructorArgumentIsNull_ThrowArgumentNullException()
        {
            //arrange
            static void action()
            {
                UserMustBeUniqueForPartnerBuilder.Create()
                    .WithUserRepository(default)
                    .Build();
            }

            //act
            Exception record = Record.Exception(action);

            //assert
            Check.That(record).IsInstanceOf<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UserMustBeUniqueForPartner_AppUserIdExists_ReturnFalse(bool userExist)
        {
            //arrange
            UserMustBeUniqueForPartnerBuilder builder = UserMustBeUniqueForPartnerBuilder.Create()
                                                            .WithUserExist(userExist);

            //act
            bool result = await builder.Build().CheckAsync(new UserRequest()).ConfigureAwait(false);

            //assert
            builder.WithReceiveUserExist(1);
            if (userExist)
            {
                Check.That(result).IsFalse();
            }
            else
            {
                Check.That(result).IsTrue();
            }
        }
    }
}