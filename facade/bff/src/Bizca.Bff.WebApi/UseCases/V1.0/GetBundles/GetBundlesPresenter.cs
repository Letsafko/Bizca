namespace Bizca.Bff.WebApi.UseCases.V10.GetBundles
{
    using Bizca.Bff.Application.UseCases.GetBundles;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>
    ///     Get bundle presenter.
    /// </summary>
    public sealed class GetBundlesPresenter : IGetBundlesOutput
    {
        /// <summary>
        ///     Get procedures view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="bundles"></param>
        public void Ok(IEnumerable<Bundle> bundles)
        {
            ViewModel = new OkObjectResult(new GetBundlesResponse(bundles));
        }
    }
}
