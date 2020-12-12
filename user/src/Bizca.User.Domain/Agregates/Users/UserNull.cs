namespace Bizca.User.Domain.Agregates.Users
{
    using Bizca.User.Domain.Agregates.Users.ValueObjects;
    using System;
    public sealed class UserNull : IUser
    {
        public static UserNull Instance { get; } = new UserNull();
        public UserCode UserCode => new UserCode(Guid.Empty);
    }
}