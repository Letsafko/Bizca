namespace Bizca.User.Domain.Agregates.ValueObjects;

using System;
using ValueOf;

public class ExternalUserId : ValueOf<string, ExternalUserId>
{
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException(@"value cannot be empty", nameof(ExternalUserId));
    }
}