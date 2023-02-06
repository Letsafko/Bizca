namespace Bizca.Bff.Application.UseCases.GetUserSubscriptions
{
    using Core.Domain.Cqrs.Queries;

    public sealed class GetUserSubscriptionsQuery : IQuery
    {
        public GetUserSubscriptionsQuery(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}