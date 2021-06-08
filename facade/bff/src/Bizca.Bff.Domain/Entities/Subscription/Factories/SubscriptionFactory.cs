namespace Bizca.Bff.Domain.Entities.Subscription.Factories
{
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using System;
    using System.Threading.Tasks;

    public sealed class SubscriptionFactory : ISubscriptionFactory
    {
        private readonly IProcedureRepository procedureRepository;
        private readonly IBundleRepository bundleRepository;
        public SubscriptionFactory(IProcedureRepository procedureRepository,
            IBundleRepository bundleRepository)
        {
            this.procedureRepository = procedureRepository;
            this.bundleRepository = bundleRepository;
        }

        public async Task<Subscription> CreateAsync(SubscriptionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
