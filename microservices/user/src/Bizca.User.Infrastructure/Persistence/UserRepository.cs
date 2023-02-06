namespace Bizca.User.Infrastructure.Persistence
{
    using Bizca.Core.Infrastructure.Database;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Repositories;
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        private const string GetByPartnerAndExternalUserIdStoredProcedure =
            "[usr].[usp_getByPartnerAndExternalUserId_user]";

        private const string GetByPartnerAndChannelResourceStoredProcedure 
            = "[usr].[usp_getByPartnerAndChannel_user]";
    
        private const string GetUserByCriteriaStoredProcedure 
            = "[usr].[usp_getByCriteria_user]";
    
        private const string IsUserExistStoredProcedure = "[usr].[usp_isExists_user]";
        private const string UpdateUserStoredProcedure = "[usr].[usp_update_user]";
        private const string CreateUserStoredProcedure = "[usr].[usp_create_user]";
    

        public async Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByPartnerIdAndChannelResourceAsync(
            int partnerId, string channelResource)
        {
            var parameters = new { partnerId, channelResource };

            SqlMapper.GridReader gridReader = await _unitOfWork.Connection
                .QueryMultipleAsync(GetByPartnerAndChannelResourceStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            var result = new Dictionary<ResultName, IEnumerable<dynamic>>();
            while (!gridReader.IsConsumed)
            {
                var resultSetName = gridReader.Read<string>().FirstOrDefault();
                if (string.IsNullOrEmpty(resultSetName)) continue;
            
                var reader = await gridReader.ReadAsync();
                if (Enum.TryParse(resultSetName, true, out ResultName resultName))
                    result[resultName] = reader;
            }

            return result;
        }

        public async Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByPartnerIdAndExternalUserIdAsync(
            int partnerId, string externalUserId)
        {
            var parameters = new { partnerId, externalUserId };

            SqlMapper.GridReader gridReader = await _unitOfWork
                .Connection
                .QueryMultipleAsync(GetByPartnerAndExternalUserIdStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);

            var result = new Dictionary<ResultName, IEnumerable<dynamic>>();
            while (!gridReader.IsConsumed)
            {
                var resultSetName = gridReader.Read<string>().FirstOrDefault();
                if (string.IsNullOrEmpty(resultSetName)) continue;
            
                IEnumerable<dynamic> reader = await gridReader.ReadAsync();
                if (Enum.TryParse(resultSetName, true, out ResultName resultName)) result[resultName] = reader;
            }

            return result;
        }

        public async Task<IEnumerable<dynamic>> GetByCriteriaAsync(int partnerId, UserCriteria criteria)
        {
            var parameters = new
            {
                direction = criteria.Direction.ToLower(),
                externalUserId = criteria.ExternalUserId,
                whatsapp = criteria.WhatsappNumber,
                pageSize = criteria.PageSize + 1,
                phone = criteria.PhoneNumber,
                birthDate = criteria.BirthDate,
                firstName = criteria.FirstName,
                lastName = criteria.LastName,
                index = criteria.PageIndex,
                email = criteria.Email,
                partnerId
            };

            return await _unitOfWork.Connection
                .QueryAsync(GetUserByCriteriaStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> IsExistAsync(int partnerId, string externalUserId)
        {
            var parameters = new { partnerId, externalUserId };

            int result = await _unitOfWork.Connection
                .ExecuteScalarAsync<int>(IsUserExistStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<int> SaveAsync(User user)
        {
            var parameters = new
            {
                externalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                economicActivityId = user.Profile.EconomicActivity?.Id,
                partnerId = user.UserIdentifier.Partner.PartnerId,
                birthCountryId = user.Profile.BirthCountry?.Id,
                civilityId = user.Profile.Civility.CivilityId,
                firstName = user.Profile.FirstName,
                birthDate = user.Profile.BirthDate,
                birthCity = user.Profile.BirthCity,
                lastName = user.Profile.LastName,
                rowversion = user.GetRowVersion()
            };

            return await _unitOfWork.Connection
                .ExecuteScalarAsync<int>(UpdateUserStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AddAsync(User user)
        {
            var parameters = new
            {
                externalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                economicActivityId = user.Profile.EconomicActivity?.Id,
                userCode = user.UserIdentifier.PublicUserCode.ToString(),
                civilityId = user.Profile.Civility.CivilityId,
                partnerId = user.UserIdentifier.Partner.PartnerId,
                birthCountryId = user.Profile.BirthCountry?.Id,
                birthDate = user.Profile.BirthDate,
                birthCity = user.Profile.BirthCity,
                firstName = user.Profile.FirstName,
                lastName = user.Profile.LastName
            };

            return await _unitOfWork.Connection
                .ExecuteScalarAsync<int>(CreateUserStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);
        }

        public async Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByIdAsync(int partnerId,
            string externalUserId)
        {
            var parameters = new { partnerId, externalUserId };

            SqlMapper.GridReader gridReader = await _unitOfWork.Connection
                .QueryMultipleAsync(GetByPartnerAndExternalUserIdStoredProcedure,
                    parameters,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);

            var result = new Dictionary<ResultName, IEnumerable<dynamic>>();
            while (!gridReader.IsConsumed)
            {
                string resultSetName = gridReader.Read<string>().FirstOrDefault();
                if (!string.IsNullOrEmpty(resultSetName))
                {
                    IEnumerable<dynamic> reader = gridReader.Read();
                    if (Enum.TryParse(resultSetName, true, out ResultName resultName)) result[resultName] = reader;
                }
            }

            return result;
        }
    }
}