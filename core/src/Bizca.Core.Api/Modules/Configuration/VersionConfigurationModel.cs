namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;

    public sealed class VersionConfigurationModel
    {
        public string Description { get; set; }
        
        public string Version { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }
    }
    
    public class VersionConfigurationModelValidator : AbstractValidator<VersionConfigurationModel>
    {
        public VersionConfigurationModelValidator()
        {
            RuleFor(x => x.Version).NotEmpty();
        }
    }
}