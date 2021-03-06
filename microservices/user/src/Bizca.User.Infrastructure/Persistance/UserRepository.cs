namespace Bizca.User.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
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
        private readonly IUnitOfWork unitOfWork;
        public UserRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getByPartnerAndExternalUserIdStoredProcedure = "[usr].[usp_getByPartnerAndExternalUserId_user]";
        private const string getByPartnerAndChannelResourceStoredProcedure = "[usr].[usp_getByPartnerAndChannel_user]";
        private const string getUserByCriteriaStoredProcedure = "[usr].[usp_getByCriteria_user]";
        private const string isUserExistStoredProcedure = "[usr].[usp_isExists_user]";
        private const string updateUserStoredProcedure = "[usr].[usp_update_user]";
        private const string createUserStoredProcedure = "[usr].[usp_create_user]";

        public async Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByPartnerIdAndChannelResourceAsync(int partnerId, string channelResource)
        {
            var parameters = new
            {
                partnerId,
                channelResource
            };

            SqlMapper.GridReader gridReader = await unitOfWork.Connection
                    .QueryMultipleAsync(getByPartnerAndChannelResourceStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            var result = new Dictionary<ResultName, IEnumerable<dynamic>>();
            while (!gridReader.IsConsumed)
            {
                string resultSetName = gridReader.Read<string>().FirstOrDefault();
                if (!string.IsNullOrEmpty(resultSetName))
                {
                    IEnumerable<dynamic> reader = gridReader.Read();
                    if (Enum.TryParse(resultSetName, true, out ResultName resultName))
                    {
                        result[resultName] = reader;
                    }
                }
            }
            return result;
        }
        public async Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByPartnerIdAndExternalUserIdAsync(int partnerId, string externalUserId)
        {
            var parameters = new
            {
                partnerId,
                externalUserId
            };

            SqlMapper.GridReader gridReader = await unitOfWork.Connection
                    .QueryMultipleAsync(getByPartnerAndExternalUserIdStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            var result = new Dictionary<ResultName, IEnumerable<dynamic>>();
            while (!gridReader.IsConsumed)
            {
                string resultSetName = gridReader.Read<string>().FirstOrDefault();
                if (!string.IsNullOrEmpty(resultSetName))
                {
                    IEnumerable<dynamic> reader = gridReader.Read();
                    if (Enum.TryParse(resultSetName, true, out ResultName resultName))
                    {
                        result[resultName] = reader;
                    }
                }
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
                partnerId,
            };

            return await unitOfWork.Connection
                .QueryAsync(getUserByCriteriaStoredProcedure,
                        parameters,
                        unitOfWork.Transaction,
                        commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }
        public async Task<bool> IsExistAsync(int partnerId, string externalUserId)
        {
            var parameters = new
            {
                partnerId,
                externalUserId
            };

            int result = await unitOfWork.Connection
                .ExecuteScalarAsync<int>(isUserExistStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            return result > 0;
        }
        public async Task<int> UpdateAsync(User user)
        {
            var parameters = new
            {
                externalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                economicActivityId = user.Profile.EconomicActivity?.Id,
                civilityId = user.Profile.Civility.CivilityId,
                partnerId = user.UserIdentifier.Partner.Id,
                birthCountryId = user.Profile.BirthCountry.Id,
                firstName = user.Profile.FirstName,
                birthDate = user.Profile.BirthDate,
                birthCity = user.Profile.BirthCity,
                lastName = user.Profile.LastName,
                rowversion = user.GetRowVersion(),
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(updateUserStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }
        public async Task<int> AddAsync(User user)
        {
            var parameters = new
            {
                externalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                userCode = user.UserIdentifier.UserCode.ToString(),
                economicActivityId = user.Profile.EconomicActivity?.Id,
                civilityId = user.Profile.Civility.CivilityId,
                partnerId = user.UserIdentifier.Partner.Id,
                birthCountryId = user.Profile.BirthCountry.Id,
                birthDate = user.Profile.BirthDate,
                birthCity = user.Profile.BirthCity,
                firstName = user.Profile.FirstName,
                lastName = user.Profile.LastName
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(createUserStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }

        public async Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByIdAsync(int partnerId, string externalUserId)
        {
            var parameters = new
            {
                partnerId,
                externalUserId
            };

            SqlMapper.GridReader gridReader = await unitOfWork.Connection
                    .QueryMultipleAsync(getByPartnerAndExternalUserIdStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            var result = new Dictionary<ResultName, IEnumerable<dynamic>>();
            while (!gridReader.IsConsumed)
            {
                string resultSetName = gridReader.Read<string>().FirstOrDefault();
                if (!string.IsNullOrEmpty(resultSetName))
                {
                    IEnumerable<dynamic> reader = gridReader.Read();
                    if (Enum.TryParse<ResultName>(resultSetName, true, out ResultName resultName))
                    {
                        result[resultName] = reader;
                    }
                }
            }
            return result;
        }
    }
}