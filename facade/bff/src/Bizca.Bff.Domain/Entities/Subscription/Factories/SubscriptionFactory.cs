namespace Bizca.Bff.Domain.Entities.Subscription.Factories
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Threading.Tasks;

    public sealed class SubscriptionFactory : ISubscriptionFactory
    {
        private readonly IProcedureRepository procedureRepository;
        public SubscriptionFactory(IProcedureRepository procedureRepository)
        {
            this.procedureRepository = procedureRepository;
        }

        private const int SubscriptionInitialId = 0;
        public async Task<Subscription> CreateAsync(SubscriptionRequest request)
        {
            var procedure = await GetProcedureAsync(request);
            var userSubscription = new UserSubscription(request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email);

            return new Subscription(SubscriptionInitialId,
                Guid.NewGuid(),
                userSubscription,
                procedure,
                default,
                default,
                default);
        }

        #region private helpers

        private async Task<Procedure> GetProcedureAsync(SubscriptionRequest request)
        {
            return await procedureRepository.GetProcedureByTypeIdAndCodeInseeAsync(request.ProcedureTypeId, request.CodeInsee)
                ?? throw new ResourceNotFoundException($"procedureType::{request.ProcedureTypeId} with codeInsee::{request.CodeInsee} does not exist.");
        }

        #endregion
    }
}