namespace Bizca.Bff.Application.UseCases.GetUsers
{
    using Domain.Wrappers.Users.Responses;
    using System.Collections.Generic;

    public sealed class GetPagedUsersDto
    {
        public GetPagedUsersDto(IEnumerable<GetUserDto> users,
            IEnumerable<PaginationLink> relations)
        {
            Relations = relations;
            Users = users;
        }

        public IEnumerable<PaginationLink> Relations { get; }
        public IEnumerable<GetUserDto> Users { get; }
    }
}