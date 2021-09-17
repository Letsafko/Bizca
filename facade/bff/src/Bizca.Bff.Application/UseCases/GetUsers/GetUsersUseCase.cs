namespace Bizca.Bff.Application.UseCases.GetUsers
{
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class GetUsersUseCase : IQueryHandler<GetUsersQuery>
    {
        private readonly IUserProfileWrapper userProfileWrapper;
        private readonly IGetUsersOutput getUsersOutput;
        public GetUsersUseCase(IUserWrapper userWrapper,
            IGetUsersOutput getUsersOutput)
        {
            this.userProfileWrapper = userWrapper;
            this.getUsersOutput = getUsersOutput;
        }

        public async Task<Unit> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var request = GetRequest(query);
            var response = await userProfileWrapper.GetUsersByCriteriaAsync(query.PartnerCode, request);
            if (!response.Success)
            {
                getUsersOutput.Invalid(response);
                return Unit.Value;
            }

            var pagedUsers = MapTo(response.Data);
            getUsersOutput.Ok(pagedUsers);
            return Unit.Value;
        }

        #region private helpers

        private GetPagedUsersDto MapTo(UsersByCriteriaResponse response)
        {
            if (response?.Users?.Any() != true)
                return default;

            var users = response.Users.Select(x => new GetUserDto(x));
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
