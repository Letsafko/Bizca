namespace Bizca.Bff.WebApi.ViewModels
{
    using Domain.Referentials.Bundle;
    using System.Collections.Generic;

    internal sealed class BundleCollectionViewModel : List<BundleViewModel>
    {
        public BundleCollectionViewModel(IEnumerable<Bundle> bundles)
        {
            foreach (Bundle bundle in bundles) Add(new BundleViewModel(bundle));
        }
    }
}