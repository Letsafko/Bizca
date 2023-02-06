namespace Bizca.Core.Domain.Referential.Model
{
    using System.Collections.Generic;

    public sealed class EconomicActivity : ValueObject
    {
        public EconomicActivity(int id, string economicActivityCode, string description)
        {
            EconomicActivityCode = economicActivityCode;
            Description = description;
            Id = id;
        }

        public string EconomicActivityCode { get; }
        public string Description { get; }
        public int Id { get; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return EconomicActivityCode;
            yield return Id;
        }
    }
}