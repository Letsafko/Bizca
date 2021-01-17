namespace Bizca.User.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Entities.ChannelConfirmation;
    using Dapper;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class ChannelConfirmationRepository : IChannelConfirmationRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public ChannelConfirmationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getUserChannelConfirmationStoredProcedure = "[usr].[usp_get_userChannelConfirmation]";
        private const string createUserChannelConfirmationStoredProcedure = "[usr].[usp_create_userChannelConfirmation]";

        public async Task<bool> AddAsync(int userId, ChannelConfirmation channelConfirmation)
        {
            var parameters = new
            {
                userId,
                channelId = channelConfirmation.ChannelType.Id,
                expirationDate = channelConfirmation.ExpirationDate,
                codeConfirmation = channelConfirmation.CodeConfirmation
            };

            return await unitOfWork.Connection
                .ExecuteAsync(createUserChannelConfirmationStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
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

            return await unitOfWork.Connection
                    .QuerySingleOrDefaultAsync(getUserChannelConfirmationStoredProcedure,
                        parameters,
                        unitOfWork.Transaction,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
        }
    }
}