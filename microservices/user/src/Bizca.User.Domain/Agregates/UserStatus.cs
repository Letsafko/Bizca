namespace Bizca.User.Domain.Agregates
{
    using System;

    [Flags]
    public enum UserStatus
    {
        None = 0,
        Initilized = 1,
        Pending = 2,
        Validated = 4,
        Closed = 8
    }
}