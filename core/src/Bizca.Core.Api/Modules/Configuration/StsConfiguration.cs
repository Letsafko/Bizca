namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;
    using System;

    public class StsConfiguration
    {
        public string Provider { get; set; }
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public bool EnableCaching { get; set; }
        public TimeSpan CacheDuration { get; set; }
    }
    
    public class StsConfigurationValidator : AbstractValidator<StsConfiguration>
    {
        public StsConfigurationValidator()
        {
            RuleFor(x => x.CacheDuration).NotEmpty();
            RuleFor(x => x.Authority).NotEmpty();
            RuleFor(x => x.ApiSecret).NotEmpty();
            RuleFor(x => x.Provider).NotEmpty();
            RuleFor(x => x.ApiName).NotEmpty();
        }
    }
}