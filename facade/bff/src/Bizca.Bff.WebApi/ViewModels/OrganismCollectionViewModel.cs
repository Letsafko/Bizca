namespace Bizca.Bff.WebApi.ViewModels
{
    using Domain.Referentials.Procedure;
    using Domain.Referentials.Procedure.ValueObjects;
    using System.Collections.Generic;

    internal sealed class OrganismCollectionViewModel : List<OrganismViewModel>
    {
        public OrganismCollectionViewModel(Dictionary<Organism, IEnumerable<Procedure>> procedures)
        {
            foreach (KeyValuePair<Organism, IEnumerable<Procedure>> proc in procedures)
                Add(new OrganismViewModel(proc.Key, proc.Value));
        }
    }
}