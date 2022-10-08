namespace Bizca.Bff.Application.UseCases.GetUserSubscriptions
{
    using Core.Application.Queries;

    public sealed class GetUserSubscriptionsQuery : IQuery
    {
        public GetUserSubscriptionsQuery(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}