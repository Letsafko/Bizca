namespace Bizca.User.Domain.Entities.Address.Factories
{
    using BusinessCheck.AddressRule.Contract;
    using Core.Domain.Referential.Services;
    using System.Threading.Tasks;

    public sealed class AddressFactory : IAddressFactory
    {
        private readonly IReferentialService _referentialService;
        private readonly IAddressRuleEngine _addressRuleEngine;

        public AddressFactory(IAddressRuleEngine addressRuleEngine, IReferentialService referentialService)
        {
            _referentialService = referentialService;
            _addressRuleEngine = addressRuleEngine;
        }

        public async Task<Address> CreateAsync(AddressRequest request)
        {
            await _addressRuleEngine.CheckRulesAsync(request);
            
            var country = await _referentialService.GetCountryByCodeAsync(request.Country);
            
            return new Address(0,
                true,
                request.Street,
                request.City,
                request.ZipCode,
                country,
                request.Name);
        }
    }
}