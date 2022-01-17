namespace Bizca.Bff.WebApi.ViewModels
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using System.Collections.Generic;

    /// <summary>
    ///     Light procedure viewModel
    /// </summary>
    public sealed class ProcedureLightCollectionViewModel : List<ProcedureLightViewModel>
    {
        /// <summary>
        ///     Create an instance of <see cref="ProcedureLightCollectionViewModel"/>
        /// </summary>
        /// <param name="procedures"></param>
        public ProcedureLightCollectionViewModel(IEnumerable<Procedure> procedures)
        {
            foreach (var proc in procedures)
            {
                Add(new ProcedureLightViewModel(proc.ProcedureType.Id,
                    proc.Organism.Id,
                    proc.Organism.CodeInsee,
                    proc.ProcedureHref,
                    proc.ProcedureType.Label,
                    proc.ProcedureSettings));
            }
        }
    }
}
