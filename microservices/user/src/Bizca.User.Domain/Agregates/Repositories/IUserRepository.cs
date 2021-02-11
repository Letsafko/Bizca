namespace Bizca.User.Domain.Agregates.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<int> AddAsync(User user);
        Task<int> UpdateAsync(User user);
        Task<bool> IsExistAsync(int partnerId, string externalUserId);
        Task<IEnumerable<dynamic>> GetByCriteriaAsync(int partnerId, UserCriteria criteria);
        Task<(dynamic user, IEnumerable<dynamic> channels)> GetByIdAsync(int partnerId, string externalUserId);
    }
}