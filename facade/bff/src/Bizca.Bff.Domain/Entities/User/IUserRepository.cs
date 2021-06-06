namespace Bizca.Bff.Domain.Entities.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IUserRepository
    {
        Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetAsync(string externalUserId);
        Task<bool> UpdateAsync(User user);
        Task<bool> AddAsync(User user);
    }

    public enum ResultName
    {
        User,
        Subscriptions
    }
}