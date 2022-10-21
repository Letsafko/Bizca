namespace Bizca.Bff.Domain.Referential.Bundle.ValueObjects
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public class BundleIdentifier : ValueObject
    {
        public BundleIdentifier(int id, string code, string label)
        {
            Label = label;
            Code = code;
            Id = id;
        }

        public string Label { get; }
        public string Code { get; }
        public int Id { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Id;
        }
    }
}