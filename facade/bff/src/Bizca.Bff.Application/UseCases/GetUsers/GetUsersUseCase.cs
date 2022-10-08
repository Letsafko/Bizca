namespace Bizca.Bff.Application.UseCases.GetUsers
{
    using Core.Application.Queries;
    using Core.Domain;
    using Domain.Wrappers.Users;
    using Domain.Wrappers.Users.Requests;
    using Domain.Wrappers.Users.Responses;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUsersUseCase : IQueryHandler<GetUsersQuery>
    {
        private readonly IGetUsersOutput getUsersOutput;
        private readonly IUserProfileWrapper userProfileWrapper;

        public GetUsersUseCase(IUserWrapper userWrapper,
            IGetUsersOutput getUsersOutput)
        {
            userProfileWrapper = userWrapper;
            this.getUsersOutput = getUsersOutput;
        }

        public async Task<Unit> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            UsersByCriteriaRequest request = GetRequest(query);
            IPublicResponse<UsersByCriteriaResponse> response =
                await userProfileWrapper.GetUsersByCriteriaAsync(query.PartnerCode, request);
            if (!response.Success)
            {
                getUsersOutput.Invalid(response);
                return Unit.Value;
            }

            GetPagedUsersDto pagedUsers = MapTo(response.Data);
            getUsersOutput.Ok(pagedUsers);
            return Unit.Value;
        }

        #region private helpers

        private GetPagedUsersDto MapTo(UsersByCriteriaResponse response)
        {
            if (response?.Users?.Any() != true)
                return default;

            IEnumerable<GetUserDto> users = response.Users.Select(x => new GetUserDto(x));
            return new GetPagedUsersDto(users, response.Relations);
        }

        private UsersByCriteriaRequest GetRequest(GetUsersQuery query)
        {
            return new UsersByCriteriaRequest(query.ExternalUserId,
                query.PhoneNumber,
                query.Email,
                query.FirstName,
                query.LastName,
                query.PageSize);
        }

        #endregion
    }
}