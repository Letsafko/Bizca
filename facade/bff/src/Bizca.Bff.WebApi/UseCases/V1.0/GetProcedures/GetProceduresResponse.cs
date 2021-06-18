namespace Bizca.Bff.WebApi.UseCases.V10.GetProcedures
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.WebApi.ViewModels;
    using System.Collections.Generic;
    internal sealed class GetProceduresResponse : List<ProcedureViewModel>
    {
        public GetProceduresResponse(IEnumerable<Procedure> procedures)
        {
            foreach(Procedure proc in procedures)
            {
                Add(new ProcedureViewModel(proc.Organism.OrganismName,
                    proc.Organism.CodeInsee,
                    new ProcedureTypeViewModel(proc.ProcedureType.Id,
                        proc.ProcedureHref,
                        proc.ProcedureType.Label)));
            }
        }
    }
}
