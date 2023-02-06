namespace Bizca.User.Domain.Agregates.ValueObjects;

using Core.Domain;
using System.Collections.Generic;

public sealed class Password : ValueObject
{
    public Password(bool active, string passwordHash, string securityStamp)
    {
        SecurityStamp = securityStamp;
        PasswordHash = passwordHash;
        Active = active;
    }

    public bool Active { get; private set; }
    public string SecurityStamp { get; }
    public string PasswordHash { get; }

    public void Update(bool active)
    {
        Active = active;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return SecurityStamp;
        yield return PasswordHash;
        yield return Active;
    }
}