namespace Bizca.User.Application.UseCases.GetUser.List
{
    using Bizca.Core.Application.Abstracts.Paging;
    using Bizca.Core.Application.Abstracts.Queries;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Application.UseCases.GetUser.Common;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUsersUseCase : IQueryHandler<GetUsersQuery>
    {
        private readonly IGetUsersOutput _outputPort;
        private readonly IUserRepository _userRepository;
        private readonly IPartnerRepository _partnerRepository;
        public GetUsersUseCase(IGetUsersOutput outputPort, IPartnerRepository partnerRepository, IUserRepository userRepository)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _partnerRepository = partnerRepository ?? throw new ArgumentNullException(nameof(partnerRepository));
        }

        public async Task<Unit> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            Partner partner = await _partnerRepository.GetByCodeAsync(request.PartnerCode).ConfigureAwait(false);
            if (partner is null)
            {
                request.ModelState.Add(nameof(request.PartnerCode), $"partner::{request.PartnerCode} is invalid.");
            }

            if (!request.ModelState.IsValid)
            {
                _outputPort.Invalid(request.ModelState);
                return Unit.Value;
            }

            UserCriteria criteria = GetCriteria(request);
            IEnumerable<dynamic> rows = await _userRepository.GetByCriteria(partner?.Id ?? 0, criteria).ConfigureAwait(false);
            if (rows?.Any() == true)
            {
                PagedResult<GetUserDto> pagedUsers = GetPagedResult(criteria.PageSize, request, rows);
                _outputPort.Ok(pagedUsers);
                return Unit.Value;
            }

            return Unit.Value;
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
        private IEnumerable<GetUserDto> BuildDto(IEnumerable<dynamic> rows)
        {
            foreach(dynamic result in rows)
            {
                yield return
                    GetUserBuilder.Instance
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
        private PagedResult<GetUserDto> GetPagedResult(int pageSize, GetUsersQuery request, IEnumerable<dynamic> rows)
        {
            List<GetUserDto> users = BuildDto(rows)?.ToList() ?? new List<GetUserDto>();
            var pagination = new Pagination<GetUserDto>(pageSize, request.RequestPath, users);
            return pagination.GetPaged(request,
                    pagination.FirstIndex?.UserId ?? 0,
                    pagination.LastIndex?.UserId ?? 0);
        }

        #endregion
    }
}
