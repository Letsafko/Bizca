namespace Bizca.User.Infrastructure
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string createUserStoredProcedure        = "[usr].[usp_create_user]";
        private const string updateUserStoredProcedure        = "[usr].[usp_update_user]";
        private const string isUserExistStoredProcedure       = "[usr].[usp_isExists_user]";
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

            return await _unitOfWork.Connection
                    .ExecuteScalarAsync<int>(createUserStoredProcedure, 
                        parameters,
                        _unitOfWork.Transaction,
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
                birthCountryId = user.BirthCountry.Id,
                economicActivityId = user.EconomicActivity?.Id
            };

            return await _unitOfWork.Connection
                    .ExecuteScalarAsync<int>(updateUserStoredProcedure,
                        parameters,
                        _unitOfWork.Transaction,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
        }

        public async Task<dynamic> GetById(int partnerId, string externalUserId)
        {
            var parameters = new
            {
                partnerId,
                externalUserId
            };

            return await _unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getByPartnerAndExternalUserIdStoredProcedure,
                            parameters,
                            _unitOfWork.Transaction,
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

            int result = await _unitOfWork.Connection
                            .ExecuteScalarAsync<int>(isUserExistStoredProcedure,
                                parameters,
                                _unitOfWork.Transaction,
                                commandType: CommandType.StoredProcedure)
                            .ConfigureAwait(false);

            return result > 0;
        }

        public async Task<IEnumerable<dynamic>> GetByCriteria(int partnerId, UserCriteria criteria)
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

            return await _unitOfWork.Connection
                    .QueryAsync(getUserByCriteriaStoredProcedure,
                            parameters,
                            _unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
        }
    }
}
