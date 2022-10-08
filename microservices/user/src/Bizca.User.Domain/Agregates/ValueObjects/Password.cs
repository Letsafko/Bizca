namespace Bizca.User.Domain.Agregates.ValueObjects
{
    using Core.Domain;
    using System.Collections.Generic;

    public sealed class Password : ValueObject
    {
        public Password(bool active, string passwordHash, string securityStamp)
        {
            Active = active;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
        }

        public bool Active { get; private set; }
        public string PasswordHash { get; }
        public string SecurityStamp { get; }

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