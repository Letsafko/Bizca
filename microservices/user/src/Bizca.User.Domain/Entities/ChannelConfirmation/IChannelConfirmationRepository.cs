namespace Bizca.User.Domain.Entities.ChannelConfirmation
{
    using System.Threading.Tasks;

    public interface IChannelConfirmationRepository
    {
        Task<bool> AddAsync(int userId, ChannelConfirmation channelConfirmation);
        Task<dynamic> GetByIdsAsync(int userId, int channelId, string codeConfirmation);
    }
}