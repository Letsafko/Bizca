namespace Bizca.User.Domain
{
    public sealed class UserNull : IUser
    {
        public static UserNull Instance { get; } = new UserNull();
    }
}
