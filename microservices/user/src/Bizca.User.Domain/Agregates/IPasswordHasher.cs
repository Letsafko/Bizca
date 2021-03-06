namespace Bizca.User.Domain.Agregates
{
    public interface IPasswordHasher
    {
        /// <summary>
        ///     Gets hash from given password.
        /// </summary>
        /// <param name="password">given password.</param>
        (string passwordHash, string securityStamp) CreateHashPassword(string password);

        /// <summary>
        ///     Verifies whether a password matches ones.
        /// </summary>
        /// <param name="password">password to verify.</param>
        /// <param name="passwordHash">hash of stored password</param>
        /// <param name="passwordSalt">securistamp of stored password.</param>
        bool VerifyHashedPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}