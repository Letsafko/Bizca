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

    public class AntelopTokenValidator : ITokenValidator
    {
        private readonly JwtSecurityTokenHandler _jwtHandler;

        private X509Certificate2 _antelopCert;

        public AntelopTokenValidator()
        {
            _jwtHandler = new JwtSecurityTokenHandler();
        }

        public X509Certificate2 LoadCertificate(string content)
        {
            if (string.IsNullOrEmpty(content)) return null;

            _antelopCert = new X509Certificate2(Convert.FromBase64String(content));

            return _antelopCert;
        }

        public bool CanReadToken(string token)
        {
            return _jwtHandler.CanReadToken(token);
        }

        public JwtSecurityToken ValidateToken(string token,
            out List<Claim> claims)
        {
            claims = new List<Claim>();

            if (!_jwtHandler.CanReadToken(token)) throw new UnauthorizedAccessException("Invalid token format");

            JwtSecurityToken jwtToken = _jwtHandler.ReadJwtToken(token);

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

        private static void ValidateWalletIdentifier(JwtSecurityToken token, ICollection<Claim> claims)
        {
            if (token.Payload is null)
            {
                throw new UnauthorizedAccessException("Invalid payload token");
            }

            var subject = token.Payload["sub"].ToString();
            if (!token.Payload.ContainsKey("sub") ||
                !string.Equals(token.Subject, subject, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Invalid Wallet Identifier");
            }

            claims.Add(new Claim(Constants.AntelopClaimsWalletId, subject!));
        }

        private static void ValidatePartnerCode(JwtSecurityToken token, ICollection<Claim> claims)
        {
            string partnerCode = Constants.AntelopClaimsDefaultPartnerCode;

            if (token.Payload?.ContainsKey("partner_code") == true)
                partnerCode = token.Payload["partner_code"].ToString();

            claims.Add(new Claim(Constants.AntelopClaimsPartnerCode, partnerCode!));
        }

        private static X509Certificate2 ValidateDeviceCertificate(JwtSecurityToken token)
        {
            if (!token.Header.ContainsKey("x5c")) throw new UnauthorizedAccessException("Invalid Certificate");

            string headerRawString = token.Header["x5c"]?.ToString();

            if (string.IsNullOrEmpty(headerRawString)) throw new UnauthorizedAccessException("Invalid Certificate");

            JArray headerRaw = JArray.Parse(headerRawString);
            var base64String = headerRaw!.First!.Value<string>();
            
            var clientCert = new X509Certificate2(Convert.FromBase64String(base64String!));

            if (DateTime.UtcNow > clientCert.NotAfter) throw new UnauthorizedAccessException("Certificate Expired");

            return clientCert;
        }

        private void CheckOrganizationalUnit(X509Certificate2 clientCert)
        {
            if (_antelopCert == null) return;

            byte[] acSubjectKeyIdentifier = _antelopCert.Extensions[Constants.OidSubjectKey]!.RawData;
            acSubjectKeyIdentifier = acSubjectKeyIdentifier.Skip(4).ToArray();

            byte[] clientAuthorityKeyIdentifier = clientCert.Extensions[Constants.OidAuthorityKey]!.RawData;
            clientAuthorityKeyIdentifier = clientAuthorityKeyIdentifier.Skip(6).ToArray();

            if (!acSubjectKeyIdentifier.SequenceEqual(clientAuthorityKeyIdentifier))
                throw new UnauthorizedAccessException("Client certificate OU must match with the issuer identifier");
        }

        private static void ValidateJwsSignature(string tokenString, X509Certificate2 clientCert)
        {
            string[] tokenParts = tokenString.Split('.');

            var rsaPublicKey = clientCert.PublicKey.GetRSAPublicKey();
            var rsa = new RSACryptoServiceProvider();

            rsa.ImportParameters(rsaPublicKey!.ExportParameters(false));

            var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenParts[0] + '.' + tokenParts[1]));

            var rsaDeFormatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeFormatter.SetHashAlgorithm("SHA256");

            if (!rsaDeFormatter.VerifySignature(hash, FromBase64Url(tokenParts[2])))
                throw new UnauthorizedAccessException("Invalid Token signature");
        }

        private static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url
                : base64Url + "===="[(base64Url.Length % 4)..];
            
            string base64 = padded.Replace("_", "/")
                .Replace("-", "+");
            
            return Convert.FromBase64String(base64);
        }
    }
}