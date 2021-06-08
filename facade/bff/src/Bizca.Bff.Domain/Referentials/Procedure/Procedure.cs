﻿namespace Bizca.Bff.Domain.Referentials.Procedure
{
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;

    public sealed class Procedure : ValueObject
    {
        public ProcedureType ProcedureType { get; }
        public string ProcedureHref { get; }
        public Organism Organism { get; }
        public Procedure(ProcedureType procedureType,
            Organism organism,
            string procedureHref)
        {
            ProcedureHref = !string.IsNullOrWhiteSpace(procedureHref) ? procedureHref : throw new ArgumentNullException(nameof(procedureHref));
            ProcedureType = procedureType ?? throw new ArgumentNullException(nameof(procedureType));
            Organism      = organism ?? throw new ArgumentNullException(nameof(organism));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Organism?.CodeInsee;
            yield return ProcedureType?.Id;
            yield return ProcedureHref;
        }
    }
}