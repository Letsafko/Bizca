namespace Bizca.Bff.WebApi.ViewModels
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using System.Collections.Generic;
    internal sealed class OrganismViewModel
    {
        public OrganismViewModel(Organism organism, IEnumerable<Procedure> procedures)
        {
            OrganismHref = organism.OrganismHref;
            OrganismName = organism.OrganismName;
            CodeInsee = organism.CodeInsee;

            Procedures = new List<ProcedureTypeViewModel>();
            foreach (Procedure procedure in procedures)
            {
                var procedureTypeVm = new ProcedureTypeViewModel(procedure.ProcedureType.Id,
                    procedure.ProcedureHref,
                    procedure.ProcedureType.Label);

                Procedures.Add(procedureTypeVm);
            }
        }

        public string CodeInsee { get; }
        public string OrganismHref { get; }
        public string OrganismName { get; }
        public ICollection<ProcedureTypeViewModel> Procedures { get; }
    }
}