namespace Bizca.Bff.Application.UseCases.GetBundles
{
    using Domain.Referential.Bundle;
    using System.Collections.Generic;

    public interface IGetBundlesOutput
    {
        void Ok(IEnumerable<Bundle> bundles);
    }
}