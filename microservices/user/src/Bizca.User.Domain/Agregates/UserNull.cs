namespace Bizca.User.Domain.Agregates
{
    using Bizca.User.Domain.Agregates.ValueObjects;
    using System;
    public sealed class UserNull : IUser
    {
        public static UserNull Instance { get; } = new UserNull();
        public UserCode UserCode => new UserCode(Guid.Empty);
    }
}