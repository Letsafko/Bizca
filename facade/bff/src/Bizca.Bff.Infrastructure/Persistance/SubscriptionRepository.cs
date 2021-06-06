namespace Bizca.Bff.Infrastructure.Persistance
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Infrastructure.Persistance.Extensions;
    using Bizca.Core.Domain;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public SubscriptionRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string upsertSubscriptionStoredProcedure = "[bff].[usp_upsert_subscription]";
        private const string subscriptionUdt = "[bff].[subscriptions]";
        public async Task<bool> UpsertAsync(int userId, IEnumerable<Subscription> subscriptions)
        {
            var parameters = new
            {
                subscriptions = subscriptions.ToDataTable(userId, subscriptionUdt)
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(upsertSubscriptionStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}
