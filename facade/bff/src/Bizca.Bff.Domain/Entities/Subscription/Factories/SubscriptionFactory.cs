namespace Bizca.Bff.Domain.Entities.Subscription.Factories
{
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Bundle.Exceptions;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.Exceptions;
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

        private const int SubscriptionInitialId = 0;
        private const int WhatsappInitialCounter = 0;
        private const int EmailInitialCounter = 0;
        private const int SmsInitialCounter = 0;
        public async Task<Subscription> CreateAsync(SubscriptionRequest request)
        {
            (Bundle bundle, Procedure procedure) = await GetEntities(request);
            if (bundle is null)
                throw new BundleDoesNotExistException($"bundle::{request.BundleId} does not exist.");

            if (procedure is null)
                throw new ProcedureDoesNotExistException($"procedureType::{request.ProcedureTypeId} with codeInsee::{request.CodeInsee} does not exist.");

            var subscriptionSettings = new SubscriptionSettings(WhatsappInitialCounter,
                EmailInitialCounter,
                SmsInitialCounter,
                bundle.BundleSettings.TotalWhatsapp,
                bundle.BundleSettings.TotalEmail,
                bundle.BundleSettings.TotalSms);

            var userSubscription = new UserSubscription(request.FirstName, 
                request.LastName, 
                request.PhoneNumber, 
                request.Whatsapp, 
                request.Email);

            return new Subscription(SubscriptionInitialId,
                Guid.NewGuid(),
                userSubscription,
                procedure,
                bundle,
                bundle.Price,
                subscriptionSettings);
        }

        private async Task<(Bundle bundle, Procedure procedure)> GetEntities(SubscriptionRequest request)
        {
            Task<Procedure> procedureTask = procedureRepository.GetProcedureByTypeIdAndCodeInseeAsync(request.ProcedureTypeId, request.CodeInsee);
            Task<Bundle> bundleTask = bundleRepository.GetBundleByIdAsync(request.BundleId);
            await Task.WhenAll(bundleTask, procedureTask);
            return
            (
                bundleTask.Result,
                procedureTask.Result
            );
        }
    }
}
