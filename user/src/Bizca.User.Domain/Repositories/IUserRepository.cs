namespace Bizca.User.Domain.Repositories
{
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<bool> IsExistAsync(int partnerId, string appUserId);
    }
}
