namespace Bizca.User.Domain.Agregates.Repositories
{
    using Bizca.User.Domain.Agregates.ValueObjects;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPasswordRepository
    {
        Task<bool> AddAsync(int userId, ICollection<Password> passwords);
    }
}