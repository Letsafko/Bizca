namespace Bizca.User.Domain.BusinessCheck.AddressRule
{
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Rules;
    using Entities.Address;
    using System.Threading.Tasks;

    public sealed class IsAddressCountryMustExist : IAddressRule
    {
        private readonly ICountryRepository _countryRepository;

        public IsAddressCountryMustExist(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CheckResult> CheckAsync(AddressRequest request)
        {
            bool success =
                (MandatoryAddressField.Country & request.Partner.MandatoryAddressField) == 0 ||
                await _countryRepository.GetByCodeAsync(request.Country) is not null;

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("address country must exist.", nameof(request.Country));

            return new CheckResult(success, checkReport);
        }
    }
}