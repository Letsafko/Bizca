namespace Bizca.User.Domain.Agregates.Users.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserChannelRepository
    {
        Task<bool> AddAsync(int userId, IEnumerable<Channel> channels);
        Task<bool> UpdateAsync(int userId, IEnumerable<Channel> channels);
    }
}
