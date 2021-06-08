namespace Bizca.Bff.Domain.Referentials.Procedure.ValueObjects
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    public class ProcedureType : ValueObject
    {
        public string Label { get; }
        public int Id { get; }
        public ProcedureType(int id, string label)
        {
            Label = label;
            Id = id;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Label;
            yield return Id;
        }
    }
}
