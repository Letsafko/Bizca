namespace Bizca.User.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.User.Domain;
    using Bizca.User.Domain.Entities.Channel.Repositories;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using Bizca.User.Infrastructure.Extensions;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class ChannelConfirmationRepository : IChannelConfirmationRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public ChannelConfirmationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string channelCodeUdt = "[usr].[channelCodes]";
        private const string upsertUserChannelConfirmationStoredProcedure = "[usr].[usp_upsert_userChannelConfirmation]";

        public async Task<bool> UpsertAsync(int userId, ChannelType channelType, IEnumerable<ChannelConfirmation> channelConfirmations)
        {
            var parameters = new
            {
                channelCodes = new TableValueParameter(channelConfirmations.ToDataTable(userId, channelType.Id, channelCodeUdt))
            };

            return await unitOfWork.Connection
                .ExecuteAsync(upsertUserChannelConfirmationStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}