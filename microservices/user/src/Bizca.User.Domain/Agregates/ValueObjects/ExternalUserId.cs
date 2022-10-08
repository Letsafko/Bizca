namespace Bizca.User.Domain.Agregates.ValueObjects
{
    using Core.Domain;
    using System.Collections.Generic;

    public sealed class ExternalUserId : ValueObject
    {
        public ExternalUserId(string userId)
        {
            AppUserId = userId;
        }

        public string AppUserId { get; }

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