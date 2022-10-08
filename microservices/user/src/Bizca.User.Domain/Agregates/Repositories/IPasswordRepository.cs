namespace Bizca.User.Domain.Agregates.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ValueObjects;

    public interface IPasswordRepository
    {
        Task<bool> AddAsync(int userId, ICollection<Password> passwords);
    }
}