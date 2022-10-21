namespace Bizca.Bff.WebApi.UseCases.V10.GetBundles
{
    using Application.UseCases.GetBundles;
    using Domain.Referential.Bundle;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using ViewModels;

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
            ViewModel = new OkObjectResult(new BundleCollectionViewModel(bundles));
        }
    }
}