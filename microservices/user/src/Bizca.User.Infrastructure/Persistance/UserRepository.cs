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

        private const string createUserStoredProcedure = "[usr].[usp_create_user]";
        private const string updateUserStoredProcedure = "[usr].[usp_update_user]";
        private const string isUserExistStoredProcedure = "[usr].[usp_isExists_user]";
        private const string getUserByCriteriaStoredProcedure = "[usr].[usp_getByCriteria_user]";
        private const string getByPartnerAndExternalUserIdStoredProcedure = "[usr].[usp_getByPartnerAndExternalUserId_user]";

        public async Task<int> AddAsync(User user)
        {
            var parameters = new
            {
                partnerId = user.Partner.Id,
                userCode = user.UserCode.ToString(),
                externalUserId = user.ExternalUserId.ToString(),
                civilityId = user.Civility.CivilityId,
                lastName = user.LastName,
                firstName = user.FirstName,
                birthDate = user.BirthDate,
                birthCity = user.BirthCity,
                birthCountryId = user.BirthCountry.Id,
                economicActivityId = user.EconomicActivity?.Id
            };

            return await unitOfWork.Connection
                    .ExecuteScalarAsync<int>(createUserStoredProcedure,
                        parameters,
                        unitOfWork.Transaction,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
        }

        public async Task<int> UpdateAsync(User user)
        {
            var parameters = new
            {
                partnerId = user.Partner.Id,
                externalUserId = user.ExternalUserId.ToString(),
                civilityId = user.Civility.CivilityId,
                lastName = user.LastName,
                firstName = user.FirstName,
                birthDate = user.BirthDate,
                birthCity = user.BirthCity,
                rowversion = user.GetRowVersion(),
                birthCountryId = user.BirthCountry.Id,
                economicActivityId = user.EconomicActivity?.Id
            };

            return await unitOfWork.Connection
                    .ExecuteScalarAsync<int>(updateUserStoredProcedure,
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

        public async Task<IEnumerable<dynamic>> GetByCriteriaAsync(int partnerId, UserCriteria criteria)
        {
            var parameters = new
            {
                partnerId,
                email = criteria.Email,
                index = criteria.PageIndex,
                phone = criteria.PhoneNumber,
                whatsapp = criteria.WhatsappNumber,
                lastName = criteria.LastName,
                pageSize = criteria.PageSize + 1,
                birthDate = criteria.BirthDate,
                firstName = criteria.FirstName,
                externalUserId = criteria.ExternalUserId,
                direction = criteria.Direction.ToLower()
            };

            return await unitOfWork.Connection
                    .QueryAsync(getUserByCriteriaStoredProcedure,
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