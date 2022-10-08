namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Repository;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsAddressCountryMustExist : IAddressRule
    {
        private readonly ICountryRepository countryRepository;

        public IsAddressCountryMustExist(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            bool success = await countryRepository.GetByCodeAsync(request.Country).ConfigureAwait(false) != null;
            if (!success)
            {
                var failure = new DomainFailure("address country must exist.",
                    nameof(request.Country),
                    typeof(AddressCountryMustExistException));

                return new RuleResult(false, failure);
            }

            return new RuleResult(true, null);
        }
    }
}