namespace Bizca.User.Infrastructure.Persistance
{
    using Core.Infrastructure.Database;
    using Domain;
    using Domain.Entities.Channel.Repositories;
    using Domain.Entities.Channel.ValueObjects;
    using Extensions;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class ChannelConfirmationRepository : IChannelConfirmationRepository
    {
        private const string channelCodeUdt = "[usr].[channelCodes]";

        private const string upsertUserChannelConfirmationStoredProcedure =
            "[usr].[usp_upsert_userChannelConfirmation]";

        private readonly IUnitOfWork unitOfWork;

        public ChannelConfirmationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> UpsertAsync(int userId, ChannelType channelType,
            IEnumerable<ChannelConfirmation> channelConfirmations)
        {
            var parameters = new
            {
                channelCodes =
                    new TableValueParameter(channelConfirmations.ToDataTable(userId, channelType.Code,
                        channelCodeUdt))
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