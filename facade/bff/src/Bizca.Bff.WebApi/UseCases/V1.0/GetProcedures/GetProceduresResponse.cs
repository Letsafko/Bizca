namespace Bizca.Bff.WebApi.UseCases.V10.GetProcedures
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using Bizca.Bff.WebApi.ViewModels;
    using System.Collections.Generic;
    internal sealed class GetProceduresResponse : List<OrganismViewModel>
    {
        public GetProceduresResponse(Dictionary<Organism, IEnumerable<Procedure>> procedures)
        {
            foreach (KeyValuePair<Organism, IEnumerable<Procedure>> proc in procedures)
            {
                Add(new OrganismViewModel(proc.Key, proc.Value));
            }
        }
    }
}