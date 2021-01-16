namespace Bizca.User.Application.UseCases.GetUser.Detail
{
    using Bizca.Core.Application.Abstracts.Queries;
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.Users.ValueObjects;

    public sealed class GetUserDetailQuery : IQuery
    {
        public GetUserDetailQuery(string externalUserId, string partnerCode)
        {
            ModelState = new Notification();

            if (string.IsNullOrWhiteSpace(partnerCode))
                ModelState.Add(nameof(partnerCode), "partnerCode is required.");

            if (string.IsNullOrWhiteSpace(externalUserId))
                ModelState.Add(nameof(externalUserId), "externalUserId is required.");

            if(ModelState.IsValid)
            {
                PartnerCode = partnerCode;
                ExternalUserId = new ExternalUserId(externalUserId);
            }
        }
        public string PartnerCode{ get; }
        public ExternalUserId ExternalUserId { get; }
        public Notification ModelState { get; }
    }
}
