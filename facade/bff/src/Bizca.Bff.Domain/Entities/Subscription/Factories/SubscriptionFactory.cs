namespace Bizca.Bff.Domain.Entities.Subscription.Factories
{
    using Bizca.Bff.Domain.Referentials.Pricing;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using System;
    using System.Threading.Tasks;

    public sealed class SubscriptionFactory : ISubscriptionFactory
    {
        private readonly IProcedureRepository procedureRepository;
        private readonly IBundleRepository pricingRepository;
        public SubscriptionFactory(IProcedureRepository procedureRepository,
            IBundleRepository pricingRepository)
        {
            this.procedureRepository = procedureRepository;
            this.pricingRepository = pricingRepository;
        }

        public Task<Subscription> BuildAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Subscription> CreateAsync(SubscriptionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
