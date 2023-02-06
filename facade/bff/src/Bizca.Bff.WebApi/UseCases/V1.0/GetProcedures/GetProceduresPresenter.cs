namespace Bizca.Bff.WebApi.UseCases.V1._0.GetProcedures
{
    using Bizca.Bff.Application.UseCases.GetProcedures;
    using Bizca.Bff.Domain.Referential.Procedure;
    using Bizca.Bff.Domain.Referential.Procedure.ValueObjects;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>
    ///     Get procedures presenter.
    /// </summary>
    public sealed class GetProceduresPresenter : IGetProceduresOutput
    {
        /// <summary>
        ///     Get procedures view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="procedures"></param>
        public void Ok(Dictionary<Organism, IEnumerable<Procedure>> procedures)
        {
            ViewModel = new OkObjectResult(new OrganismCollectionViewModel(procedures));
        }
    }
}