namespace Bizca.Bff.Infrastructure.Persistance
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Core.Domain;
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

        private const string createUserStoredProcedure = "[bff].[usp_create_user]";
        private const string updateUserStoredProcedure = "[bff].[usp_update_user]";
        private const string getUserStoredProcedure    = "[bff].[usp_get_user]";
        public async Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetAsync(string externalUserId) 
        {
            var parameters = new
            {
                externalUserId
            };

            SqlMapper.GridReader gridReader = await unitOfWork.Connection
                    .QueryMultipleAsync(getUserStoredProcedure,
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
        public async Task<bool> UpdateAsync(User user)
        {
            var parameters = new
            {
                confirmationStatus = (short)user.UserProfile.ChannelConfirmationStatus,
                activationStatus = (short)user.UserProfile.ChannelActivationStatus,
                externalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                civilityId = (byte)user.UserProfile.Civility,
                phoneNumber = user.UserProfile.PhoneNumber,
                firstName = user.UserProfile.FirstName,
                lastName = user.UserProfile.LastName,
                whatsapp = user.UserProfile.Whatsapp,
                email = user.UserProfile.Email,
                rowversion = user.GetRowVersion()       
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(updateUserStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
        public async Task<bool> AddAsync(User user)
        {
            var parameters = new
            {
                confirmationStatus = (short)user.UserProfile.ChannelConfirmationStatus,
                activationStatus = (short)user.UserProfile.ChannelActivationStatus,
                externalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                civilityId = (byte)user.UserProfile.Civility,
                phoneNumber = user.UserProfile.PhoneNumber,
                firstName = user.UserProfile.FirstName,
                lastName = user.UserProfile.LastName,
                whatsapp = user.UserProfile.Whatsapp,
                email = user.UserProfile.Email,
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(createUserStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}