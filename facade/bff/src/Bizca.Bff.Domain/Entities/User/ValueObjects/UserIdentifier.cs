namespace Bizca.Bff.Domain.Entities.User.ValueObjects
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class UserIdentifier : ValueObject
    {
        public UserIdentifier(string externalUserId, string partnerCode)
        {
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
        }

        public string ExternalUserId { get; }
        public string PartnerCode { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ExternalUserId;
            yield return PartnerCode;
        }
    }
}
