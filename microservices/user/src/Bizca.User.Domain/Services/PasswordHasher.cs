namespace Bizca.User.Domain.Agregates;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public sealed class PasswordHasher : IPasswordHasher
{
    public (string passwordHash, string securityStamp) CreateHashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return (Convert.ToBase64String(passwordHash),
            Convert.ToBase64String(passwordSalt));
    }

    public bool VerifyHashedPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
    }
}