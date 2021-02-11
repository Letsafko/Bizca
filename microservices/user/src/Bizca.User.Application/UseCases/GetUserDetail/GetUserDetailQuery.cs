namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using Bizca.Core.Application.Queries;

    public sealed class GetUserDetailQuery : IQuery
    {
        public GetUserDetailQuery(string externalUserId, string partnerCode)
        {
            PartnerCode = partnerCode;
            ExternalUserId = externalUserId;
        }
        public string PartnerCode { get; }
        public string ExternalUserId { get; }
    }
}