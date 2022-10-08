namespace Bizca.Core.Security.Antelop.Middleware
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Options;

    /// <summary>Antelop ASP.NET Core Post Configure Options</summary>
    public class AntelopPostConfigureOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly AspNetAntelopSecurityTokenValidator tokenValidator;

        /// <summary>Initializes a new instance of the <see cref="AntelopPostConfigureOptions" /> class.</summary>
        /// <param name="tokenValidator">The token validator.</param>
        public AntelopPostConfigureOptions(AspNetAntelopSecurityTokenValidator tokenValidator)
        {
            this.tokenValidator = tokenValidator;
        }

        /// <summary>Invoked to configure a <span class="typeparameter">TOptions</span> instance.</summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configured.</param>
        public void PostConfigure(string name, JwtBearerOptions options)
        {
            if (name != Constants.ANTELOP_SCHEME) return;

            options.SecurityTokenValidators.Clear();
            options.SecurityTokenValidators.Add(tokenValidator);
        }
    }
}