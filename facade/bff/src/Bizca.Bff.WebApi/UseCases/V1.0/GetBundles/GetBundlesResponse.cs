namespace Bizca.Bff.WebApi.UseCases.V10.GetBundles
{
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.WebApi.ViewModels;
    using System.Collections.Generic;

    internal sealed class GetBundlesResponse : List<BundleViewModel>
    {
        public GetBundlesResponse(IEnumerable<Bundle> bundles)
        {
            foreach(Bundle bundle in bundles)
            {
                Add(new BundleViewModel(bundle));
            }
        }
    }
}
