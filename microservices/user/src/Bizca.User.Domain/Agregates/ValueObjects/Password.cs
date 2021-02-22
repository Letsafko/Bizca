namespace Bizca.User.Domain.Agregates.ValueObjects
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class Password : ValueObject
    {
        public bool Active { get; private set; }
        public string PasswordHash { get; }
        public string SecurityStamp { get; }
        public Password(bool active, string passwordHash, string securityStamp)
        {
            Active = active;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
        }

        public void Update(bool active)
        {
            Active = active;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Active;
            yield return PasswordHash;
            yield return SecurityStamp;
        }
    }
}