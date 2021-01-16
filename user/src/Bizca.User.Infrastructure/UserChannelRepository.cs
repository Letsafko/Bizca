namespace Bizca.User.Infrastructure
{
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure.Abstracts.Database;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using Bizca.User.Infrastructure.Extensions;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class UserChannelRepository : IUserChannelRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserChannelRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string channelUdt = "[usr].[channelList]";
        private const string createUserChannelStoredProcedure = "[usr].[usp_create_userChannel]";
        private const string updateUserChannelStoredProcedure = "[usr].[usp_update_userChannel]";

        public async Task<bool> AddAsync(int userId, IEnumerable<Channel> channels)
        {
            var parameters = new
            {
                channels = new TableValueParameter(channels.ToDataTable(userId, channelUdt))
            };

            return await _unitOfWork.Connection
                .ExecuteAsync(createUserChannelStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }

        public async Task<bool> UpdateAsync(int userId, IEnumerable<Channel> channels)
        {
            var parameters = new
            {
                channels = new TableValueParameter(channels.ToDataTable(userId, channelUdt))
            };

            return await _unitOfWork.Connection
                    .ExecuteAsync(updateUserChannelStoredProcedure,
                        parameters,
                        _unitOfWork.Transaction,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false) > 0;
        }
    }
}
