namespace Bizca.Core.Domain.Referential.Model
{
    using System.Collections.Generic;

    public sealed class Civility : ValueObject
    {
        public Civility(int civilityId, string civilityCode)
        {
            CivilityCode = civilityCode;
            CivilityId = civilityId;
        }

        public string CivilityCode { get; }
        public int CivilityId { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CivilityCode;
            yield return CivilityId;
        }
    }
}