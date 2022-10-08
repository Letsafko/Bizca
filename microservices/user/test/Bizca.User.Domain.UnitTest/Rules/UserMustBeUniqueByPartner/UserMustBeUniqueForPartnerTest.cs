namespace Bizca.User.Domain.UnitTest.Rules.UserMustBeUniqueByPartner
{
    using Bizca.Core.Domain;
    using Bizca.Core.Support.Test.Builders;
    using Bizca.User.Domain.Agregates;
    using Core.Domain.Referential.Model;
    using NFluent;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class UserMustBeUniqueForPartnerTest
    {
        [Fact]
        public void UserMustBeUniqueForPartner_AnyConstructorArgumentIsNull_ThrowArgumentNullException()
        {
            Check.ThatCode(() => UserMustBeUniqueByPartnerBuilder.Instance.WithUserRepository(default).Build())
                 .Throws<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UserMustBeUniqueForPartner_AppUserIdExists_ReturnFalse(bool userExist)
        {
            //arrange
            UserMustBeUniqueByPartnerBuilder builder = UserMustBeUniqueByPartnerBuilder.Instance.WithUserExist(userExist);

            //act
            Partner partner = PartnerBuilder.Instance.Build();
            RuleResult result = await builder.Build().CheckAsync(new UserRequest { Partner = partner }).ConfigureAwait(false);

            //assert
            builder.WithReceiveUserExist(1);
            Check.That(result.Sucess).Equals(!userExist);
            Check.That(string.IsNullOrWhiteSpace(result.Failure?.ErrorMessage)).Equals(!userExist);
        }
    }
}