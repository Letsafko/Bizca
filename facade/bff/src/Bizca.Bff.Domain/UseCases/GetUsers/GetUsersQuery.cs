namespace Bizca.Bff.Application.UseCases.GetUsers
{
    using Core.Domain.Cqrs.Queries;

    public sealed class GetUsersQuery : IQuery
    {
        public GetUsersQuery(string partnerCode,
            string externalUserId,
            string phoneNumber,
            string email,
            string firstName,
            string lastName,
            int pageSize)
        {
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            PageSize = pageSize;
            Email = email;
        }

        public string ExternalUserId { get; }
        public string PhoneNumber { get; }
        public string PartnerCode { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public int PageSize { get; }
    }
}