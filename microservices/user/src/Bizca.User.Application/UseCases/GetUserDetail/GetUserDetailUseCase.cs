namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using Bizca.Core.Application.Queries;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates.Repositories;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUserDetailUseCase : IQueryHandler<GetUserDetailQuery>
    {
        private readonly IUserRepository userRepository;
        private readonly IGetUserDetailOutput outputPort;
        private readonly IReferentialService referentialService;
        public GetUserDetailUseCase(IReferentialService referentialService,
            IUserRepository userRepository,
            IGetUserDetailOutput outputPort)
        {
            this.outputPort = outputPort;
            this.userRepository = userRepository;
            this.referentialService = referentialService;
        }

        public async Task<Unit> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            Dictionary<ResultName, IEnumerable<dynamic>> resultDico = await userRepository.GetByPartnerIdAndExternalUserIdAsync(partner.Id, request.ExternalUserId).ConfigureAwait(false);
            dynamic user = resultDico[ResultName.User].FirstOrDefault();
            if (user is null)
            {
                outputPort.NotFound($"no user associated to '{request.ExternalUserId}' exists.");
                return Unit.Value;
            }

            dynamic address = resultDico[ResultName.Addresses].SingleOrDefault(x => (bool)x.active);
            GetUserDetail getUserDetail = await GetUserDetailAsync(user, address).ConfigureAwait(false);
            outputPort.Ok(getUserDetail);
            return Unit.Value;
        }

        private async Task<(EconomicActivity economicActivity, Country birthCountry, Civility civility)>
            GetReferentialAsync(int? economicActivityId, int? birthCountryId, int civilityId)
        {
            var economicActivityTask = Task.FromResult(default(EconomicActivity));
            if (economicActivityId.HasValue)
            {
                economicActivityTask = referentialService.GetEconomicActivityByIdAsync(economicActivityId.Value);
            }

            var birthCountryTask = Task.FromResult(default(Country));
            if (birthCountryId.HasValue)
            {
                birthCountryTask = referentialService.GetCountryByIdAsync(birthCountryId.Value);
            }

            Task<Civility> civilityTask = referentialService.GetCivilityByIdAsync(civilityId, true);
            await Task.WhenAll(civilityTask, birthCountryTask, economicActivityTask).ConfigureAwait(false);
            return
            (
                economicActivityTask.Result,
                birthCountryTask.Result,
                civilityTask.Result
            );
        }

        private async Task<GetUserDetail> GetUserDetailAsync(dynamic result, dynamic address)
        {
            (EconomicActivity economicActivity, Country birthCountry, Civility civility) =
                await GetReferentialAsync((int?)result.economicActivityId, (int?)result.birthCountryId, (int)result.civilityId);

            dynamic builder = GetUserDetailBuilder.Instance
                .WithUserId(result.userId)
                .WithUserCode(result.userCode.ToString())
                .WithExternalUserId(result.externalUserId)
                .WithEmail(result.email, result.emailActive, result.emailConfirmed)
                .WithPhoneNumber(result.phone, result.phoneActive, result.phoneConfirmed)
                .WithWhatsapp(result.whatsapp, result.whatsappActive, result.whatsappConfirmed)
                .WithLastName(result.lastName)
                .WithFirstName(result.firstName)
                .WithBirthCity(result.birthCity)
                .WithCivility(civility.CivilityCode)
                .WithBirthCountry(birthCountry?.CountryCode)
                .WithBirthDate(result.birthDate?.ToString("yyyy-MM-dd"))
                .WithEconomicActivity(economicActivity?.EconomicActivityCode);

            if(address != null)
            {
                builder.WithAddress(address.addressId,
                   address.active,
                   address.street,
                   address.city,
                   address.zipcode,
                   address.countryId,
                   address.countryCode,
                   address.description,
                   address.addressName);
            }
            return builder.Build();
        }
    }
}