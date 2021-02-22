namespace Bizca.User.Domain.Entities.Address.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAddressRepository
    {
        Task<bool> UpsertAsync(int userId, IEnumerable<Address> addresses);
    }
}