namespace Bizca.Core.Domain.Test
{
    using NFluent;
    using Xunit;

    public sealed class ResponseTest
    {
        [Fact]
        public void Initialize_ctor_set_properties()
        {
            //arrange
            const int value = 1;
            const int errorCode = 1;
            const int statusCode = 1;
            const string errorMessage = "error message";

            //act 
            var result = new Response<int>(value, errorMessage, errorCode, statusCode);

            //assert
            Check.That(result.Data).Equals(value);
            Check.That(result.ErrorCode).Equals(errorCode);
            Check.That(result.StatusCode).Equals(statusCode);
            Check.That(result.ErrorMessage).Equals(errorMessage);
            Check.That(result.Data).IsInstanceOfType(value.GetType());
        }
    }
}