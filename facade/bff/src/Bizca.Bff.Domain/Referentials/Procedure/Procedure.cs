namespace Bizca.Bff.Domain.Referentials.Procedure
{
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using System;

    public sealed class Procedure
    {
        public ProcedureType ProcedureType { get; }
        public string ProcedureHref { get; }
        public Organism Organism { get; }
        public Procedure(ProcedureType procedureType,
            Organism organism,
            string procedureHref)
        {
            ProcedureHref = string.IsNullOrWhiteSpace(procedureHref) ? procedureHref : throw new ArgumentNullException(nameof(procedureHref));
            ProcedureType = procedureType ?? throw new ArgumentNullException(nameof(procedureType));
            Organism      = organism ?? throw new ArgumentNullException(nameof(organism));
        }
    }
}