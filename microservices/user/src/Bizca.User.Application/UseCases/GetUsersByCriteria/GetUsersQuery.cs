namespace Bizca.User.Application.UseCases.GetUsersByCriteria
{
    using Core.Application.Queries;
    using System.Collections.Generic;

    public sealed class GetUsersQuery : IQuery<IEnumerable<GetUsers>>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Whatsapp { get; set; }
        public string FirstName { get; set; }
        public string BirthDate { get; set; }
        public string PartnerCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ExternalUserId { get; set; }
        public string Direction { get; set; }
    }

    public static class SearchDirection
    {
        public const string Next = "next";
        public const string Previous = "previous";
    }
}