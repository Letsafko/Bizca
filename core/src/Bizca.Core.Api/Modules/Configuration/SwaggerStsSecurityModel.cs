namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;
    using System.Collections.Generic;

    public sealed class SwaggerStsSecurityModel
    {
        public IEnumerable<string> Scopes { get; set; }
        
        public string ClientSecret { get; set; }
        
        public string ClientId { get; set; }

        public string StsUrl { get; set; }
    }
    
    public class SwaggerStsSecurityModelValidator : AbstractValidator<SwaggerStsSecurityModel>
    {
        public SwaggerStsSecurityModelValidator()
        {
            RuleFor(x => x.ClientSecret).NotEmpty();
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.StsUrl).NotEmpty();
            RuleFor(x => x.Scopes).NotEmpty();
        }
    }
}