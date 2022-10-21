namespace Bizca.Bff.Domain.Referential.Bundle
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBundleRepository
    {
        Task<IEnumerable<Bundle>> GetBundlesAsync();
        Task<Bundle> GetBundleByIdAsync(int bundleId);
    }
}