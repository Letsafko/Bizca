namespace Bizca.Bff.Domain.Referentials.Procedure.ValueObjects
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    public sealed class Organism : ValueObject
    {
        public string OrganismName { get; }
        public string OrganismHref { get; }
        public string CodeInsee { get; }
        public Organism(string codeInsee,
            string organismName,
            string organismHref)
        {
            OrganismName = organismName;
            OrganismHref = organismHref;
            CodeInsee    = codeInsee;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return OrganismName;
            yield return OrganismHref;
            yield return CodeInsee;
        }
    }
}