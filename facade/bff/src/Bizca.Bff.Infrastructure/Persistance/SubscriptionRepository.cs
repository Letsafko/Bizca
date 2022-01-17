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

        private const string UpdateSubscriberAvailabilityStoredProcedure = "[bff].[usp_update_subscriptionAvailability]";
        private const string GetProcedureSubscriberStoredProcedure = "[bff].[usp_getByProcedure_subscribers]";
        private const string UpsertSubscriptionStoredProcedure = "[bff].[usp_upsert_subscription]";
        private const string SubscriptionAvailabilityUdt = "[bff].[subscriptionAvailabilityUdt]";
        private const string SubscriptionUdt = "[bff].[subscriptionsUdt]";
        public async Task<bool> UpsertAsync(int userId, IEnumerable<Subscription> subscriptions)
        {
            var parameters = new
            {
                userId,
                subscriptions = subscriptions.ToDataTable(SubscriptionUdt)
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(UpsertSubscriptionStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }


        public async Task<IEnumerable<SubscriberAvailability>> GetSubscribers(int organismId, int procedureTypeId)
        {
            var parameters = new
            {
                procedureTypeId,
                organismId
            };

            var results = await unitOfWork.Connection
                .QueryAsync(GetProcedureSubscriberStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);

            var list = new List<SubscriberAvailability>();
            foreach (var result in results)
            {
                var user = new UserSubscription(result.firstName,
                    result.lastName,
                    result.phoneNumber,
                    null,
                    result.email);

                var availability = new SubscriberAvailability(user,
                    result.subscriptionId,
                    result.emailCounter,
                    result.smsCounter);

                list.Add(availability);
            }

            return list;
        }


        public async Task<bool> UpdateSubscriberAvailability(IEnumerable<SubscriberAvailability> subscribers)
        {
            var parameters = new
            {
                subscriptions = subscribers.ToDataTable(SubscriptionAvailabilityUdt)
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(UpdateSubscriberAvailabilityStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}