namespace Bizca.Core.Security.Antelop
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>Antelop Token Validator</summary>
    public class AntelopTokenValidator : ITokenValidator
    {
        private readonly JwtSecurityTokenHandler jwtHandler;

        private X509Certificate2 antelopCert;

        /// <summary>Initializes a new instance of the <see cref="AntelopTokenValidator" /> class.</summary>
        public AntelopTokenValidator()
        {
            jwtHandler = new JwtSecurityTokenHandler();
        }

        #region ITokenValidator members

        /// <summary>Loads the certifcate.</summary>
        /// <param name="content">The content.</param>
        public X509Certificate2 LoadCertificate(string content)
        {
            if (string.IsNullOrEmpty(content)) return null;

            antelopCert = new X509Certificate2(Convert.FromBase64String(content));

            return antelopCert;
        }

        /// <summary>Determines whether this instance [can read token] the specified token.</summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///     <c>true</c> if this instance [can read token] the specified token; otherwise, <c>false</c>.
        /// </returns>
        public bool CanReadToken(string token)
        {
            return jwtHandler.CanReadToken(token);
        }

        /// <summary>Validates the token.</summary>
        /// <param name="token">The token.</param>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        public JwtSecurityToken ValidateToken(string token,
            out List<Claim> claims)
        {
            claims = new List<Claim>();

            if (!jwtHandler.CanReadToken(token)) throw new UnauthorizedAccessException("Invalid token format");

            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);

            //Validate the Wallet identifier
            ValidateWalletIdentifier(jwtToken, claims);

            //Validate and extract partner code
            ValidatePartnerCode(jwtToken, claims);

            //Validate the device certificate
            X509Certificate2 clientCertificate = ValidateDeviceCertificate(jwtToken);

            //Check Organizational Unit
            CheckOrganizationalUnit(clientCertificate);

            //Validate the JWS signature
            ValidateJwsSignature(token, clientCertificate);

            return jwtToken;
        }

        #endregion

        #region Private methods

        /// <summary>Validates the wallet identifier.</summary>
        /// <param name="token">The token.</param>
        /// <param name="claims">The claims.</param>
        /// <exception cref="UnauthorizedAccessException">Invalid Wallet Identifier</exception>
        private void ValidateWalletIdentifier(JwtSecurityToken token, List<Claim> claims)
        {
            // Check that the Subject’s Common Name in the certificate matches 
            // with the Wallet ID in the payload ("sub" field)
            if (token.Payload?.ContainsKey("sub") == false
                || token.Subject != token.Payload["sub"].ToString())
                throw new UnauthorizedAccessException("Invalid Wallet Identifier");

            claims.Add(new Claim(Constants.ANTELOP_CLAIMS_WALLET_ID, token.Payload["sub"].ToString()));
        }

        private void ValidatePartnerCode(JwtSecurityToken token, List<Claim> claims)
        {
            string partnerCode = Constants.ANTELOP_CLAIMS_DEFAULT_PARTNER_CODE;

            if (token.Payload?.ContainsKey("partner_code") == true)
                partnerCode = token.Payload["partner_code"].ToString();

            //TODO: Define where in the payload to extract the partner code
            claims.Add(new Claim(Constants.ANTELOP_CLAIMS_PARTNER_CODE, partnerCode));
        }

        /// <summary>Validates the device certificate.</summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException">
        ///     Invalid Certificate
        ///     or
        ///     Invalid Certificate
        ///     or
        ///     Certificate Expired
        /// </exception>
        private X509Certificate2 ValidateDeviceCertificate(JwtSecurityToken token)
        {
            if (!token.Header.ContainsKey("x5c")) throw new UnauthorizedAccessException("Invalid Certificate");

            string headerRawString = token.Header["x5c"]?.ToString();

            if (string.IsNullOrEmpty(headerRawString)) throw new UnauthorizedAccessException("Invalid Certificate");

            JArray headerRaw = JArray.Parse(headerRawString);

            var clientCert = new X509Certificate2(Convert.FromBase64String(headerRaw.First.Value<string>()));

            if (DateTime.UtcNow > clientCert.NotAfter) throw new UnauthorizedAccessException("Certificate Expired");

            //Check with the Antelop CA certificate’s public key that the client certificate is valid

            return clientCert;
        }

        /// <summary>
        ///     (Optional) Check that client certificate’s Organizational Unit name matches with your issuer identifier (given
        ///     during onboarding)
        ///     This step can avoid rareful collisions between the wallet identifiers of 2 Antelop clients
        /// </summary>
        /// <param name="clientCert">The client cert.</param>
        private void CheckOrganizationalUnit(X509Certificate2 clientCert)
        {
            if (antelopCert == null) return;

            byte[] acSubjectKeyIdentifier = antelopCert.Extensions[Constants.OID_SUBJECT_KEY].RawData;
            acSubjectKeyIdentifier = acSubjectKeyIdentifier.Skip(4).ToArray();

            byte[] clientAuthorityKeyIdentifier = clientCert.Extensions[Constants.OID_AUTHORITY_KEY].RawData;
            clientAuthorityKeyIdentifier = clientAuthorityKeyIdentifier.Skip(6).ToArray();

            if (!acSubjectKeyIdentifier.SequenceEqual(clientAuthorityKeyIdentifier))
                throw new UnauthorizedAccessException("Client certicate OU must match with the issuer identifier");
        }

        /// <summary>Validates the JWS signature.</summary>
        /// <param name="tokenString">The token string.</param>
        /// <param name="clientCert">The client cert.</param>
        /// <exception cref="UnauthorizedAccessException">Invalid Token signature</exception>
        private void ValidateJwsSignature(string tokenString, X509Certificate2 clientCert)
        {
            //Split the JWS token in 3 parts
            string[] tokenParts = tokenString.Split('.');

            var config = (RSACng)clientCert.PublicKey.Key;
            var rsa = new RSACryptoServiceProvider();

            rsa.ImportParameters(config.ExportParameters(false));

            //Concatenate the base64URL-encoded header, a dot and the base64URL-encoded payload
            var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenParts[0] + '.' + tokenParts[1]));

            //Apply a RSASSA-PKCS1-v1_5 SHA-256 signature (if "alg" is "RS256") with the Wallet public key
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA256");

            //Compare the result with the 3rd part of the JWS token (signature)
            if (!rsaDeformatter.VerifySignature(hash, FromBase64Url(tokenParts[2])))
                throw new UnauthorizedAccessException("Invalid Token signature");
        }

        /// <summary>Froms the base64 URL.</summary>
        /// <param name="base64Url">The base64 URL.</param>
        /// <returns></returns>
        private static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url
                : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }

        #endregion
    }
}