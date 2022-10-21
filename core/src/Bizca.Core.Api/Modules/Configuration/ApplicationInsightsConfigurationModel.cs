namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;
    using Microsoft.ApplicationInsights.AspNetCore.Extensions;

    public class ApplicationInsightsConfigurationModel : ApplicationInsightsServiceOptions
    {
        public string ApplicationName { get; set; }
    }
    
    public class ApplicationInsightsConfigurationModelValidator : AbstractValidator<ApplicationInsightsConfigurationModel>
    {
        public ApplicationInsightsConfigurationModelValidator()
        {
            RuleFor(x => x.ApplicationName).NotEmpty();
        }
    }
}