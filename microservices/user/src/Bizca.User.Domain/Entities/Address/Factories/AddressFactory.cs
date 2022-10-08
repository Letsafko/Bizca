namespace Bizca.User.Domain.Entities.Address.Factories
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Entities.Address.BusinessCheck;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class AddressFactory : IAddressFactory
    {
        private readonly IAddressRuleEngine addressRuleEngine;
        private readonly IReferentialService referentialService;
        public AddressFactory(IAddressRuleEngine addressRuleEngine, IReferentialService referentialService)
        {
            this.addressRuleEngine = addressRuleEngine;
            this.referentialService = referentialService;
        }

        public async Task<Address> CreateAsync(AddressRequest request)
        {
            RuleResultCollection ruleResults = await addressRuleEngine.CheckRulesAsync(request).ConfigureAwait(false);
            ManageResultChecks(ruleResults);

            Country country = await referentialService.GetCountryByCodeAsync(request.Country).ConfigureAwait(false);
            return new Address(0,
                true,
                request.Street,
                request.City,
                request.ZipCode,
                country,
                request.Name);
        }

        private void ManageResultChecks(RuleResultCollection collection)
        {
            foreach (RuleResult rule in collection)
            {
                if (!rule.Sucess)
                {
                    throw Activator.CreateInstance(rule.Failure.ExceptionType, new List<DomainFailure> { rule.Failure }) as DomainException;
                }
            }
        }
    }
}