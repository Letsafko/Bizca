namespace Bizca.User.Domain.Agregates;

using System;

[Flags]
public enum UserStatus
{
    None = 0,
    Initialized = 1,
    Pending = 2,
    Validated = 4,
    Closed = 8
}