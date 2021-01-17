namespace Bizca.User.Domain.Agregates
{
    using Bizca.User.Domain.Agregates.ValueObjects;
    public interface IUser
    {
        /// <summary>
        ///     Gets user code.
        /// </summary>
        public UserCode UserCode { get; }
    }
}