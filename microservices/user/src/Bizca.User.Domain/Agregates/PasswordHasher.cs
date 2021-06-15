namespace Bizca.User.Domain.Agregates
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class PasswordHasher : IPasswordHasher
    {
        public (string passwordHash, string securityStamp) CreateHashPassword(string password)
        {
            byte[] passwordHash, passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            return (Convert.ToBase64String(passwordHash),
                    Convert.ToBase64String(passwordSalt));
        }
        public bool VerifyHashedPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
    }
}