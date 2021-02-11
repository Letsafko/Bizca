namespace Bizca.Core.Security.Antelop
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>Token validator interface</summary>
    public interface ITokenValidator
    {
        /// <summary>Loads the certificate.</summary>
        /// <param name="content">The content.</param>
        X509Certificate2 LoadCertificate(string content);

        /// <summary>Determines whether this instance [can read token] the specified token.</summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can read token] the specified token; otherwise, <c>false</c>.</returns>
        bool CanReadToken(string token);

        /// <summary>Validates the token.</summary>
        /// <param name="token">The token.</param>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        JwtSecurityToken ValidateToken(string token, out List<System.Security.Claims.Claim> claims);
    }
}