namespace Bizca.Bff.Domain.Entities.User
{
    using System.Threading.Tasks;
    public interface IUserRepository
    {
        Task<User> GetAsync(string externalUserId);
        Task<bool> UpdateAsync(User user);
        Task<bool> AddAsync(User user);
    }

    public enum ResultName
    {
        User,
        Subscriptions
    }
}