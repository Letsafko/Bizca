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
        private readonly IGetUsersOutput getUsersOutput;
        private readonly IUserWrapper userWrapper;
        public GetUsersUseCase(IUserWrapper userWrapper,
            IGetUsersOutput getUsersOutput)
        {
            this.getUsersOutput = getUsersOutput;
            this.userWrapper = userWrapper;
        }

        public async Task<Unit> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var request = GetRequest(query);
            var response = await userWrapper.GetUsersByCriteriaAsync(query.PartnerCode, request);

            var pagedUsers = MapTo(response);
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
                query.LastName);
        }

        #endregion
    }
}
