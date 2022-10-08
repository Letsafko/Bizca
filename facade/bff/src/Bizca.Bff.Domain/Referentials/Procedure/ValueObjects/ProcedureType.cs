namespace Bizca.Bff.Domain.Referentials.Procedure.ValueObjects
{
    using Core.Domain;
    using System.Collections.Generic;

    public class ProcedureType : ValueObject
    {
        public ProcedureType(int id, string label)
        {
            Label = label;
            Id = id;
        }

        public string Label { get; }
        public int Id { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Label;
            yield return Id;
        }
    }
}