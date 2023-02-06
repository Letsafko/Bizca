namespace Bizca.Bff.WebApi.UseCases.V1._0.GetActiveProcedures
{
    using Bizca.Bff.Application.UseCases.GetActiveProcedures;
    using Bizca.Bff.Domain.Referential.Procedure;
    using Bizca.Bff.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>
    ///     Get procedures for active subscriptions
    /// </summary>
    public class GetActiveProceduresPresenter : IGetActiveProceduresOutput
    {
        /// <summary>
        ///     Get procedures for active subscriptions view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="procedures"></param>
        public void Ok(IEnumerable<Procedure> procedures)
        {
            ViewModel = new OkObjectResult(new ProcedureLightCollectionViewModel(procedures));
        }
    }
}