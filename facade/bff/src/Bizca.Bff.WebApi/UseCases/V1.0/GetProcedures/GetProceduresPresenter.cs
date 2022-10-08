namespace Bizca.Bff.WebApi.UseCases.V10.GetProcedures
{
    using Application.UseCases.GetProcedures;
    using Domain.Referentials.Procedure;
    using Domain.Referentials.Procedure.ValueObjects;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using ViewModels;

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