namespace Bizca.Bff.Domain.Referentials.Bundle
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBundleRepository
    {
        Task<IEnumerable<Bundle>> GetBundlesAsync();
        Task<Bundle> GetBundleByIdAsync(int bundleId);
    }
}