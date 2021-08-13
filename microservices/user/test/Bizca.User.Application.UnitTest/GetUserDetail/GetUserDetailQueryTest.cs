namespace Bizca.User.Application.UnitTest.GetUser.Detail
{
    using Bizca.User.Application.UseCases.GetUserDetail;
    using NFluent;
    using Xunit;

    public sealed class GetUserDetailQueryTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(default)]
        public void PartnerCode_NullOrEmpty_ModelStateError(string partnerCode)
        {
            const string message = "partnerCode is required.";
            var query = new GetUserDetailQuery("test", partnerCode);
            //Check.That(query.ModelState.Errors).Not.IsEmpty().And
            //     .CountIs(1).And
            //     .ContainsPair(nameof(partnerCode), new string[] { message });
        }

        [Theory]
        [InlineData("")]
        [InlineData(default)]
        public void ExternalUserId_NullOrEmpty_ModelStateError(string externalUserId)
        {
            const string message = "externalUserId is required.";
            var query = new GetUserDetailQuery(externalUserId, "bizca");
            //Check.That(query.ModelState.IsValid).IsFalse();
            //Check.That(query.ModelState.Errors).Not.IsEmpty().And
            //     .CountIs(1).And
            //     .ContainsPair(nameof(externalUserId), new string[] { message });
        }

        [Fact]
        public void ArgumentAllNotNull_Return_Query()
        {
            var query = new GetUserDetailQuery("test", "bizca");
            //Check.That(query.ModelState.IsValid).IsTrue();
            Check.That(query.PartnerCode).Equals(query.PartnerCode);
            Check.That(query.ExternalUserId).Equals(query.ExternalUserId);
        }
    }
}