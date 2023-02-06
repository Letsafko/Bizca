namespace Bizca.User.Infrastructure.Persistence
{
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
        private readonly IUnitOfWork _unitOfWork;

        public ChannelRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        private const string SaveUserChannelStoredProcedure = "[usr].[usp_upsert_userChannel]";
        private const string IsChannelExistStoredProcedure = "[usr].[usp_isExists_channel]";
        private const string ChannelUdt = "[usr].[channelList]";

        public async Task<bool> SaveAsync(int userId, IEnumerable<Channel> channels)
        {
            var parameters = new
            {
                channels = new TableValueParameter(channels.ToDataTable(userId, ChannelUdt))
            };

            return await _unitOfWork.Connection
                .ExecuteAsync(SaveUserChannelStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }

        public async Task<bool> IsExistAsync(int partnerId, string channelResource)
        {
            return await _unitOfWork.Connection
                .ExecuteScalarAsync<int>(IsChannelExistStoredProcedure,
                    new { partnerId, channelResource },
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}