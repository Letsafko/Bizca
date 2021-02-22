namespace Bizca.User.Application.UseCases.GetUsersByCriteria
{
    using Bizca.Core.Application.Queries;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUsersUseCase : IQueryHandler<GetUsersQuery, IEnumerable<GetUsers>>
    {
        private readonly IUserRepository userRepository;
        private readonly IReferentialService referentialService;
        public GetUsersUseCase(IReferentialService referentialService,
            IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.referentialService = referentialService;
        }

        public async Task<IEnumerable<GetUsers>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode).ConfigureAwait(false);
            if (partner is null)
            {
                return default;
            }

            UserCriteria criteria = GetCriteria(request);
            IEnumerable<dynamic> rows = await userRepository.GetByCriteriaAsync(partner.Id, criteria).ConfigureAwait(false);
            return rows?.Any() == false
                ? default
                : GetUsers(rows);
        }

        #region private helpers

        private UserCriteria GetCriteria(GetUsersQuery request)
        {
            return new UserCriteria
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                WhatsappNumber = request.Whatsapp,
                LastName = request.LastName,
                FirstName = request.FirstName,
                BirthDate = request.BirthDate,
                ExternalUserId = request.ExternalUserId,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Direction = request.Direction
            };
        }
        private IEnumerable<GetUsers> GetUsers(IEnumerable<dynamic> rows)
        {
            foreach (dynamic result in rows)
            {
                yield return
                    GetUsersBuilder.Instance
                        .WithUserId(result.userId)
                        .WithUserCode(result.userCode.ToString())
                        .WithExternalUserId(result.externalUserId)
                        .WithEmail(result.email, result.emailActive, result.emailConfirmed)
                        .WithPhoneNumber(result.phone, result.phoneActive, result.phoneConfirmed)
                        .WithWhatsapp(result.whatsapp, result.whatsappActive, result.whatsappConfirmed)
                        .WithCivility(result.civilityCode)
                        .WithLastName(result.lastName)
                        .WithFirstName(result.firstName)
                        .WithBirthCity(result.birthCity)
                        .WithBirthDate(result.birthDate.ToString("yyyy-MM-dd"))
                        .WithBirthCountry(result.birthCountryCode)
                        .WithEconomicActivity(result.economicActivityCode)
                        .WithAddress(result.addressId,
                            result.addresseActive,
                            result.addresseStreet,
                            result.addresseCity,
                            result.zipcode,
                            result.countryId,
                            result.countryCode,
                            result.description,
                            result.addressName)
                        .Build();
            }
        }

        #endregion
    }
}