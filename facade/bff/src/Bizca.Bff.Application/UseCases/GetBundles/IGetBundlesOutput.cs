﻿namespace Bizca.Bff.Application.UseCases.GetBundles
{
    using Bizca.Bff.Domain.Referentials.Bundle;
    using System.Collections.Generic;
    public interface IGetBundlesOutput
    {
        void Ok(IEnumerable<Bundle> bundles);
    }
}