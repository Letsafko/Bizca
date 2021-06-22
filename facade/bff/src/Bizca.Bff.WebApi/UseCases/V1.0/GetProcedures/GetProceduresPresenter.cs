namespace Bizca.Bff.WebApi.UseCases.V10.GetProcedures
{
    using Bizca.Bff.Application.UseCases.GetProcedures;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
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
            ViewModel = new OkObjectResult(new GetProceduresResponse(procedures));
        }
    }
}