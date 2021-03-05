namespace Bizca.User.Domain.Agregates.ValueObjects
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class ExternalUserId : ValueObject
    {
        public string AppUserId { get; }
        public ExternalUserId(string userId)
        {
            AppUserId = userId;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AppUserId;
        }
        public override string ToString()
        {
            return AppUserId;
        }
    }
}