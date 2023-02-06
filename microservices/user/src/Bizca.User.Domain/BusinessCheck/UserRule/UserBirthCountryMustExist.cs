namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class UserBirthCountryMustExist : IUserRule
    {
        private readonly ICountryRepository _countryRepository;

        public UserBirthCountryMustExist(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success = (MandatoryUserProfileField.BirthCounty & request.Partner.MandatoryUserProfileField) == 0 ||
                           await _countryRepository.GetByCodeAsync(request.BirthCountry) is not null;

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport($"given birth country '{request.BirthCountry}' does not exist.",
                    nameof(request.BirthCountry));

            return new CheckResult(success, checkReport);
        }
    }
}