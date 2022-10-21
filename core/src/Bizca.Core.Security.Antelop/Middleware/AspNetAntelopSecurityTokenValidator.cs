namespace Bizca.Core.Security.Antelop.Middleware
{
    using Configuratin;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>Midlleware Antelop Security Token Validator</summary>
    public class AspNetAntelopSecurityTokenValidator : ISecurityTokenValidator
    {
        private readonly ITokenValidator tokenValidator;

        /// <summary>Initializes a new instance of the <see cref="AspNetAntelopSecurityTokenValidator" /> class.</summary>
        /// <param name="tokenValidator">The token validator.</param>
        public AspNetAntelopSecurityTokenValidator(ITokenValidator tokenValidator,
            IOptions<AntelopConfiguration> configuration)
        {
            this.tokenValidator = tokenValidator;
            this.tokenValidator.LoadCertificate(configuration.Value?.AntelopCertificate);
        }

        /// <summary>Returns true if a token can be validated.</summary>
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        /// <summary>Returns true if the token can be read, false otherwise.</summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        public bool CanReadToken(string securityToken)
        {
            return tokenValidator.CanReadToken(securityToken);
        }

        /// <summary>
        ///     Validates a token passed as a string using
        ///     <see cref="T:Microsoft.IdentityModel.Tokens.TokenValidationParameters">TokenValidationParameters</see>
        /// </summary>
        /// <param name="securityToken"></param>
        /// <param name="validationParameters"></param>
        /// <param name="validatedToken"></param>
        /// <returns></returns>
        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            //Validate Token
            JwtSecurityToken jwtToken = tokenValidator.ValidateToken(securityToken,
                out List<Claim> claims);
            validatedToken = jwtToken;

            //Extract claims
            Claim walletIdClaim =
                claims.SingleOrDefault(claim => claim.Type == Constants.AntelopClaimsWalletId);
            if (walletIdClaim != null) claims.Add(new Claim(Constants.AspnetClaimsUserId, walletIdClaim.Value));
            Claim partnerCodeClaim =
                claims.SingleOrDefault(claim => claim.Type == Constants.AntelopClaimsPartnerCode);
            if (partnerCodeClaim != null)
                claims.Add(new Claim(Constants.AspnetClaimsPartnerCode, partnerCodeClaim.Value));


            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Antelop"));
        }
    }
}