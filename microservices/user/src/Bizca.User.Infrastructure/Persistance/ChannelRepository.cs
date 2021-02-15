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

        private const string channelUdt = "[usr].[channelList]";
        private const string upSertUserChannelStoredProcedure = "[usr].[usp_upsert_userChannel]";
        public async Task<bool> UpSertAsync(int userId, IEnumerable<Channel> channels)
        {
            var parameters = new
            {
                channels = new TableValueParameter(channels.ToDataTable(userId, channelUdt))
            };

            return await unitOfWork.Connection
                .ExecuteAsync(upSertUserChannelStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}