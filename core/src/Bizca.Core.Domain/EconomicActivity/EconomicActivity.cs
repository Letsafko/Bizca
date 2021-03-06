namespace Bizca.Core.Domain.EconomicActivity
{
    using System.Collections.Generic;
    public sealed class EconomicActivity : ValueObject
    {
        public string EconomicActivityCode { get; }
        public string Description { get; }
        public int Id { get; }
        public EconomicActivity(int id, string economicActivityCode, string description)
        {
            EconomicActivityCode = economicActivityCode;
            Description = description;
            Id = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return EconomicActivityCode;
            yield return Description;
            yield return Id;
        }
    }
}