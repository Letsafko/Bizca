namespace Bizca.User.Domain.Agregates.Users.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<dynamic> GetById(int partnerId, string externalUserId);
        Task<bool> IsExistAsync(int partnerId, string externalUserId);
        Task<IEnumerable<dynamic>> GetByCriteria(int partnerId, UserCriteria criteria);
    }
}