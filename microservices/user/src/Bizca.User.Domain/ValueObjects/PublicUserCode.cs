namespace Bizca.User.Domain.Agregates.ValueObjects;

using System;
using ValueOf;

public sealed class PublicUserCode : ValueOf<Guid, PublicUserCode>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
            throw new ArgumentException(@"value cannot be empty", nameof(PublicUserCode));
    }
}