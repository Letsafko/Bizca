namespace Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails
{
    using Core.Domain.Cqrs.Queries;

    public sealed class GetUserSubscriptionDetailsQuery : IQuery
    {
        public GetUserSubscriptionDetailsQuery(string externalUserId,
            string subscriptionCode)
        {
            SubscriptionCode = subscriptionCode;
            ExternalUserId = externalUserId;
        }

        public string SubscriptionCode { get; }
        public string ExternalUserId { get; }
    }
}