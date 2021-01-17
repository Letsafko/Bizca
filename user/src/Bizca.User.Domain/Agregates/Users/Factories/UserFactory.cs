namespace Bizca.User.Domain.Agregates.Users.Factories
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using Bizca.User.Domain.Agregates.Users.Rules;
    using Bizca.User.Domain.Agregates.Users.ValueObjects;
    using System;
    using System.Threading.Tasks;

    public sealed class UserFactory : IUserFactory
    {
        #region fields & ctor

        private readonly IUserRuleEngine _userRuleEngine;
        private readonly IUserRepository _userRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICivilityRepository _civilityRepository;
        private readonly IEconomicActivityRepository _economicActivityRepository;
        public UserFactory(IUserRuleEngine userRuleEngine,
            IUserRepository userRepository,
            ICountryRepository countryRepository,
            ICivilityRepository civilityRepository,
            IEconomicActivityRepository economicActivityRepository)
        {
            _userRuleEngine = userRuleEngine ?? throw new ArgumentNullException(nameof(userRuleEngine));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _civilityRepository = civilityRepository ?? throw new ArgumentNullException(nameof(civilityRepository));
            _economicActivityRepository = economicActivityRepository ?? throw new ArgumentNullException(nameof(economicActivityRepository));
        }

        #endregion

        public async Task<IUser> BuildAsync(Partner partner, string externalUserId)
        {
            dynamic result = await _userRepository.GetById(partner.Id, externalUserId).ConfigureAwait(false);
            return result is null
                ? UserNull.Instance
                : UserBuilder.Instance
                .WithPartner(partner)
                .WithId(result.userId)
                .WithUserCode(new UserCode(result.userCode))
                .WithExternalUserId(new ExternalUserId(result.externalUserId))
                .WithLastName(result.lastName)
                .WithFisrtName(result.firstName)
                .WithBirthCity(result.birthCity)
                .WithBirthDate(result.birthDate)
                .WithEmail(result.email, result.emailActive, result.emailConfirmed)
                .WithPhoneNumber(result.phone, result.phoneActive, result.phoneConfirmed)
                .WithWhatsapp(result.whatsapp, result.whatsappActive, result.whatsappConfirmed)
                .WithCivility(new Civility(result.civilityId, result.civilityCode))
                .WithBirthCountry(new Country(result.birthCountryId, result.birthCountryCode, result.birthCountryDescription))
                .WithEconomicActivity(new EconomicActivity(result.economicActivityId, result.economicActivityCode, result.economicActivityDescription))
                .Build() as IUser;
        }
        public async Task<Response<IUser>> CreateAsync(UserRequest request)
        {
            var notification = new Notification();
            RuleResultCollection collection = await _userRuleEngine.CheckRulesAsync(request).ConfigureAwait(false);
            ManageResultChecks(notification, collection);

            Civility civility = await _civilityRepository.GetByIdAsync(request.Civility).ConfigureAwait(false);
            if(civility is null)
            {
                notification.Add(nameof(civility), $"civility::{civility} is invalid.");
            }

            Country birthCountry = await _countryRepository.GetByCodeAsync(request.BirthCountry).ConfigureAwait(false);
            if (birthCountry is null)
            {
                notification.Add(nameof(birthCountry), $"birthCountry::{birthCountry} is invalid.");
            }

            EconomicActivity economicActivity = await _economicActivityRepository.GetByIdAsync(request.EconomicActivity).ConfigureAwait(false);
            if (economicActivity is null)
            {
                notification.Add(nameof(economicActivity), $"economicActivity::{economicActivity} is invalid.");
            }

            if(!notification.IsValid)
            {
                var response = new Response<IUser>(UserNull.Instance);
                response.ModelState.Add(notification.Errors);
                return response;
            }

            User user = UserBuilder.Instance
                    .WithUserCode(new UserCode(Guid.NewGuid()))
                    .WithPartner(request.Partner)
                    .WithExternalUserId(new ExternalUserId(request.ExternalUserId))
                    .WithCivility(civility)
                    .WithBirthCountry(birthCountry)
                    .WithEconomicActivity(economicActivity)
                    .WithBirthCity(request.BirthCity)
                    .WithBirthDate(request.BirthDate.Value)
                    .WithEmail(request.Email)
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithFisrtName(request.FirstName)
                    .WithLastName(request.LastName)
                    .WithWhatsapp(request.Whatsapp)
                    .Build();

            return new Response<IUser>(user);
        }
        public async Task<IUser> UpdateAsync(UserRequest request)
        {
            dynamic result = await _userRepository.GetById(request.Partner.Id, request.ExternalUserId).ConfigureAwait(false);
            if(result is null)
            {
                return UserNull.Instance;
            }

            UserBuilder builder = UserBuilder.Instance
                .WithPartner(request.Partner)
                .WithId(result.userId)
                .WithUserCode(new UserCode(result.userCode))
                .WithExternalUserId(new ExternalUserId(result.externalUserId))
                .WithLastName(!string.IsNullOrWhiteSpace(request.LastName) ? request.LastName : result.lastName)
                .WithFisrtName(!string.IsNullOrWhiteSpace(request.FirstName) ? request.FirstName : result.firstName)
                .WithBirthCity(!string.IsNullOrWhiteSpace(request.BirthCity) ? request.BirthCity : result.birthCity)
                .WithBirthDate(request.BirthDate ?? result.birthDate);

            if (!string.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(result.email, StringComparison.OrdinalIgnoreCase))
                builder.WithEmail(request.Email);
            else
                builder.WithEmail(result.email, result.emailActive, result.emailConfirmed);

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && !request.PhoneNumber.Equals(result.phone, StringComparison.OrdinalIgnoreCase))
                builder.WithPhoneNumber(request.PhoneNumber);
            else
                builder.WithPhoneNumber(result.phone, result.phoneActive, result.phoneConfirmed);

            if (!string.IsNullOrWhiteSpace(request.Whatsapp) && !request.Whatsapp.Equals(result.whatsapp, StringComparison.OrdinalIgnoreCase))
                builder.WithWhatsapp(request.Whatsapp);
            else
                builder.WithWhatsapp(result.whatsapp, result.whatsappActive, result.whatsappConfirmed);

            Civility civility = await _civilityRepository.GetByIdAsync(request.Civility).ConfigureAwait(false);
            builder.WithCivility(civility ?? new Civility(result.civilityId, result.civilityCode));

            Country birthCountry = await _countryRepository.GetByCodeAsync(request.BirthCountry).ConfigureAwait(false);
            builder.WithBirthCountry(birthCountry ?? new Country(result.birthCountryId, result.birthCountryCode, result.birthCountryDescription));

            EconomicActivity economicActivity = await _economicActivityRepository.GetByIdAsync(request.EconomicActivity).ConfigureAwait(false);
            builder.WithEconomicActivity(economicActivity ?? new EconomicActivity(result.economicActivityId, result.economicActivityCode, result.economicActivityDescription));

            return builder.Build();
        }

        private void ManageResultChecks(Notification notification, RuleResultCollection collection)
        {
            foreach(RuleResult check in collection)
            {
                if (!check.Sucess)
                {
                    notification.Add(check.ExceptionType.Name, check.Message);
                    //throw Activator.CreateInstance(check.ExceptionType, check.Message) as DomainException;
                }
            }
        }
    }
}