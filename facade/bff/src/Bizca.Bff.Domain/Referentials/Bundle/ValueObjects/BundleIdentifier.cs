namespace Bizca.Bff.Domain.Referentials.Pricing.ValueObjects
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    public class BundleIdentifier : ValueObject
    {
        public string Label { get; }
        public string Code { get; }
        public int Id { get; }
        public BundleIdentifier(int id, string code, string label)
        {
            Label = label;
            Code = code;
            Id = id;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Id;
        }
    }
}
