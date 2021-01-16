namespace Bizca.User.Infrastructure
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using Dapper;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class UserChannelConfirmationRepository : IUserChannelConfirmationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserChannelConfirmationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string getUserChannelConfirmationStoredProcedure    = "[usr].[usp_get_userChannelConfirmation]";
        private const string createUserChannelConfirmationStoredProcedure = "[usr].[usp_create_userChannelConfirmation]";

        public async Task<bool> AddAsync(int userId, ChannelCodeConfirmation channelConfirmation)
        {
            var parameters = new
            {
                userId,
                channelId = channelConfirmation.Channel,
                expirationDate = channelConfirmation.ExpirationDate,
                codeConfirmation = channelConfirmation.CodeConfirmation
            };

            return await _unitOfWork.Connection
                .ExecuteAsync(createUserChannelConfirmationStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }

        public async Task<dynamic> GetByIdsAsync(int userId, int channelId, string confirmationCode)
        {
            var parameters = new
            {
                userId,
                channelId,
                confirmationCode
            };

            return await _unitOfWork.Connection
                    .QuerySingleOrDefaultAsync(getUserChannelConfirmationStoredProcedure,
                        parameters,
                        _unitOfWork.Transaction,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
        }
    }
}
