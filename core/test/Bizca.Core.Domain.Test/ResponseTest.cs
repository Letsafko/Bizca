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
            const bool success = true;

            //act 
            var result = new Response<int>(value);

            //assert
            Check.That(result.Value).Equals(value);
            Check.That(result.Success).Equals(success);
            Check.That(result.Value).IsInstanceOfType(value.GetType());
        }
    }
}
