namespace Bizca.User.Domain.Agregates.Users.Repositories
{
    using System.Threading.Tasks;

    public interface IUserChannelConfirmationRepository
    {
        Task<bool> AddAsync(int userId, ChannelCodeConfirmation channelConfirmation);
        Task<dynamic> GetByIdsAsync(int userId, int channelId, string codeConfirmation);
    }
}
