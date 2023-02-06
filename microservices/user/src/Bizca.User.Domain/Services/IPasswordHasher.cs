namespace Bizca.User.Domain.Agregates;

public interface IPasswordHasher
{
    (string passwordHash, string securityStamp) CreateHashPassword(string password);

    /// <summary>
    ///     Verifies whether a password matches ones.
    /// </summary>
    /// <param name="password">password to verify.</param>
    /// <param name="passwordHash">hash of stored password</param>
    /// <param name="passwordSalt">security stamp of stored password.</param>
    bool VerifyHashedPassword(string password, byte[] passwordHash, byte[] passwordSalt);
}