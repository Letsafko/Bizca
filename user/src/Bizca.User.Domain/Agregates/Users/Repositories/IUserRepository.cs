namespace Bizca.User.Domain.Agregates.Users.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<int> AddAsync(User user);
        Task<int> UpdateAsync(User user);
        Task<dynamic> GetById(int partnerId, string externalUserId);
        Task<bool> IsExistAsync(int partnerId, string externalUserId);
        Task<IEnumerable<dynamic>> GetByCriteria(int partnerId, UserCriteria criteria);
    }
}