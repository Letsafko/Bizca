namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using Bizca.Core.Application.Queries;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates.Repositories;
    using MediatR;
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
            dynamic result = await userRepository.GetByIdAsync(partner.Id, request.ExternalUserId).ConfigureAwait(false);
            if (result != null)
            {
                outputPort.Ok(GetUserDetail(result));
                return Unit.Value;
            }

            outputPort.NotFound();
            return Unit.Value;
        }

        private GetUserDetail GetUserDetail(dynamic result)
        {
            return GetUserDetailBuilder.Instance
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
                    .Build();
        }
    }
}