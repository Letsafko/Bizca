namespace Bizca.User.Domain.Agregates.Users
{
    public interface IUser
    {
        /// <summary>
        ///     Gets user code.
        /// </summary>
        public UserCode UserCode { get; }
    }
}