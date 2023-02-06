namespace Bizca.Bff.Application.UseCases.GetUserDetails
{
    using Core.Domain.Cqrs.Queries;

    public sealed class GetUserDetailsQuery : IQuery
    {
        public GetUserDetailsQuery(string partnerCode, string externalUserId)
        {
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
        }

        public string ExternalUserId { get; }
        public string PartnerCode { get; }
    }
}