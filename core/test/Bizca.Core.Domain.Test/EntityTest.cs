using NFluent;
using Xunit;

namespace Bizca.Core.Domain.Test
{
    public sealed class EntityTest
    {
        [Fact]
        public void Initialize_ctor_set_properties()
        {
            //arrange
            const int id = 1;

            //act
            var obj = new FakeEntity(id);

            //assert
            Check.That(obj.Id).Equals(id);
            Check.That(obj.Id).IsInstanceOfType(id.GetType());
        }
    }

    public sealed class FakeEntity : Entity
    {
        public FakeEntity(int id)
        {
            Id = id;
        }
    }
}