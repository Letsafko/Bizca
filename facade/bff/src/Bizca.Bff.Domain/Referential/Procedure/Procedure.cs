﻿namespace Bizca.Bff.Domain.Referential.Procedure
{
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;
    using ValueObjects;

    public sealed class Procedure : ValueObject
    {
        public Procedure(ProcedureType procedureType,
            Organism organism,
            string procedureHref,
            string procedureConfiguration = default)
        {
            ProcedureHref = !string.IsNullOrWhiteSpace(procedureHref)
                ? procedureHref
                : throw new ArgumentNullException(nameof(procedureHref));
            ProcedureType = procedureType ?? throw new ArgumentNullException(nameof(procedureType));
            Organism = organism ?? throw new ArgumentNullException(nameof(organism));
            ProcedureConfiguration = procedureConfiguration;
        }

        public string ProcedureConfiguration { get; }
        public ProcedureType ProcedureType { get; }
        public string ProcedureHref { get; }
        public Organism Organism { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Organism?.CodeInsee;
            yield return ProcedureType?.Id;
            yield return ProcedureHref;
        }
    }
}