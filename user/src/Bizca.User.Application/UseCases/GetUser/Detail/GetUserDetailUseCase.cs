namespace Bizca.User.Application.UseCases.GetUser.Detail
{
    using Bizca.Core.Application.Abstracts.Queries;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Application.UseCases.GetUser.Common;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUserDetailUseCase : IQueryHandler<GetUserDetailQuery>
    {
        private readonly IGetUserDetailOutput _outputPort;
        private readonly IUserRepository _userRepository;
        private readonly IPartnerRepository _partnerRepository;
        public GetUserDetailUseCase(IGetUserDetailOutput outputPort, IPartnerRepository partnerRepository, IUserRepository userRepository)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _partnerRepository = partnerRepository ?? throw new ArgumentNullException(nameof(partnerRepository));
        }

        public async Task<Unit> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            Partner partner = await _partnerRepository.GetByCodeAsync(request.PartnerCode).ConfigureAwait(false);
            if (partner is null)
                request.ModelState.Add(nameof(request.PartnerCode), $"partner::{request.PartnerCode} is invalid.");

            if (!request.ModelState.IsValid)
            {
                _outputPort.Invalid(request.ModelState);
                return Unit.Value;
            }

            dynamic result = await _userRepository.GetById(partner?.Id ?? 0, request.ExternalUserId.AppUserId).ConfigureAwait(false);
            if (result != null)
            {
                _outputPort.Ok(BuildDto(result));
                return Unit.Value;
            }

            _outputPort.NotFound();
            return Unit.Value;
        }

        private GetUserDto BuildDto(dynamic result)
        {
            return GetUserBuilder.Instance
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
