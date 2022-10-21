namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;
    using System.Collections.Generic;

    public sealed class SwaggerConfigurationModel
    {
        public IEnumerable<SwaggerSecurityDefinitionModel> Security { get; set; }
        
        public IEnumerable<VersionConfigurationModel> Versions { get; set; }

        public IEnumerable<string> XmlDocumentations { get; set; }

        public SwaggerStsSecurityModel StsSecurity { get; set; }
    }
    
    public class SwaggerConfigurationModelValidator : AbstractValidator<SwaggerConfigurationModel>
    {
        public SwaggerConfigurationModelValidator()
        {
            RuleForEach(x => x.Security)
                .SetValidator(new SwaggerSecurityDefinitionModelValidator());
            
            RuleForEach(x => x.Versions)
                .SetValidator(new VersionConfigurationModelValidator());
            
            RuleFor(x => x.StsSecurity)
                .SetValidator(new SwaggerStsSecurityModelValidator());
        }
    }
}