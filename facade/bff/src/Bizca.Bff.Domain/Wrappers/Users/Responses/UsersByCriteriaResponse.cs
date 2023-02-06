namespace Bizca.Bff.Domain.Wrappers.Users.Responses
{
    using System.Collections.Generic;

    public sealed class UsersByCriteriaResponse
    {
        public IEnumerable<PaginationLink> Relations { get; set; }
        public IEnumerable<UserResponse> Users { get; set; }
    }
}