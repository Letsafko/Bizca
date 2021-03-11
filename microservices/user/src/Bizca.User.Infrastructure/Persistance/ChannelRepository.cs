namespace Bizca.User.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.Repositories;
    using Bizca.User.Infrastructure.Extensions;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class ChannelRepository : IChannelRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public ChannelRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string UpSertUserChannelStoredProcedure = "[usr].[usp_upsert_userChannel]";
        private const string IsChannelExistStoredProcedure = "[usr].[usp_isExists_channel]";
        private const string channelUdt = "[usr].[channelList]";
        public async Task<bool> UpSertAsync(int userId, IEnumerable<Channel> channels)
        {
            var parameters = new
            {
                channels = new TableValueParameter(channels.ToDataTable(userId, channelUdt))
            };

            return await unitOfWork.Connection
                .ExecuteAsync(UpSertUserChannelStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }

        public async Task<bool> IsExistAsync(int partnerId, string channelResource)
        {
            var parameters = new
            {
                partnerId,
                channelResource
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(IsChannelExistStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}