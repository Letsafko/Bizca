namespace Bizca.Bff.Domain.Entities.User
{
    using System.Threading.Tasks;
    public interface IUserRepository
    {
        Task<dynamic> GetAsync(string externalUserId);
        Task<bool> UpdateAsync(User user);
        Task<bool> AddAsync(User user);
    }
}