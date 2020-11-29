using System;

namespace Bizca.User.Domain.Agregates.Users
{
    public sealed class UserNull : IUser
    {
        public static UserNull Instance { get; } = new UserNull();
        public UserCode UserCode => new UserCode(Guid.Empty);
    }
}