namespace Bizca.Core.Domain.Test
{
    using NFluent;
    using System.Collections.Generic;
    using Xunit;

    public sealed class ValueObjectTest
    {
        [Theory]
        [InlineData("equal", "equal", true)]
        [InlineData("equal", "notequal", false)]
        public void Same_property_value_shouldbe_equal(string propOne, string propTwo, bool expected)
        {
            //arrage
            var objOne = new FakeValueObject(1, true, propOne);
            var objTwo = new FakeValueObject(1, true, propTwo);

            //assert
            if (expected)
                Check.That(objOne).IsEqualTo(objTwo);
            else
                Check.That(objOne).IsNotEqualTo(objTwo);
        }
    }

    public class FakeValueObject : ValueObject
    {
        public int FakePropertyInt { get; }
        public bool FakePropertyBool { get; }
        public string FakePropertyString { get; }
        public FakeValueObject(int propInt, bool propBool, string propString)
        {
            FakePropertyInt = propInt;
            FakePropertyBool = propBool;
            FakePropertyString = propString;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FakePropertyInt;
            yield return FakePropertyBool;
            yield return FakePropertyString;
        }
    }
}