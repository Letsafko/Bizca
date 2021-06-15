namespace Bizca.Bff.Domain.Entities.User.ValueObjects
{
    using Bizca.Bff.Domain.Properties;
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class UserIdentifier : ValueObject
    {
        public UserIdentifier(int userId, string externalUserId)
        {
            ExternalUserId = externalUserId;
            UserId = userId;
        }

        public string PartnerCode { get; } = Resources.PartnerCode;
        public string ExternalUserId { get; }
        public int UserId { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ExternalUserId;
            yield return UserId;
        }
    }
}