namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;

    public sealed class VersioningConfigurationModel
    {
        public string Default { get; set; }

        public string RouteConstraintName { get; set; }
    }
    
    public class VersioningConfigurationModelValidator : AbstractValidator<VersioningConfigurationModel>
    {
        public VersioningConfigurationModelValidator()
        {
            RuleFor(x => x.RouteConstraintName).NotEmpty();
            RuleFor(x => x.Default).NotEmpty();
        }
    }
}