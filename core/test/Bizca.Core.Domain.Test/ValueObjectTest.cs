namespace Bizca.Core.Domain.Test
{
    using NFluent;
    using System.Collections.Generic;
    using Xunit;

    public sealed class ValueObjectTest
    {
        [Fact(DisplayName = "Should return true if all properties have same value")]
        public void Should_true_if_all_properties_have_same_value()
        {
            //arrange
            var objOne = new FakeValueObject(1, true, "same value");
            var objTwo = new FakeValueObject(1, true, "same value");

            //assert
             Check.That(objOne).IsEqualTo(objTwo);
        }
        
        [Fact(DisplayName = "Should return false if one of properties has different value")]
        public void Should_true_if_one_of_properties_has_different_value()
        {
            //arrange
            var objOne = new FakeValueObject(1, true, "same value");
            var objTwo = new FakeValueObject(1, true, "different value");

            //assert
            Check.That(objOne).HasSameValueAs(objTwo);
            Check.That(objOne).IsNotEqualTo(objTwo);
        }
    }

    public class FakeValueObject : ValueObject
    {
        public FakeValueObject(int propInt, bool propBool, string propString)
        {
            FakePropertyInt = propInt;
            FakePropertyBool = propBool;
            FakePropertyString = propString;
        }

        public int FakePropertyInt { get; }
        public bool FakePropertyBool { get; }
        public string FakePropertyString { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FakePropertyInt;
            yield return FakePropertyBool;
            yield return FakePropertyString;
        }
    }
}