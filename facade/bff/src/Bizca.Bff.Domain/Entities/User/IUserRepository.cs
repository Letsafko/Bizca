namespace Bizca.Bff.Domain.Entities.User
{
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> GetByExternalUserIdAsync(string externalUserId);
        Task<User> GetByPhoneNumberAsync(string phoneNumber);
        Task<User> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(User user);
        Task<bool> AddAsync(User user);
    }

    public enum ResultName
    {
        User,
        Subscriptions
    }
}