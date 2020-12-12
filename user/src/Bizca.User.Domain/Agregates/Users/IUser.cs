namespace Bizca.User.Domain.Agregates.Users
{
    using Bizca.User.Domain.Agregates.Users.ValueObjects;
    public interface IUser
    {
        /// <summary>
        ///     Gets user code.
        /// </summary>
        public UserCode UserCode { get; }
    }
}