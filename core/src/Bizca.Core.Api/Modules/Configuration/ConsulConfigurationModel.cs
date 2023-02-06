namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;
    using System;

    public sealed class ConsulConfigurationModel
    {
        public ConsulConfigurationModel()
        {
            HealthCheckEndPoint = "health";
        }

        public string HealthCheckEndPoint { get; set; }
        
        public string ServiceName { get; set; }

        public Uri SystemAddress { get; set; }
        
        public Uri ConsulHost { get; set; }

        public string Token { get; set; }
    }
    
    public class ConsulConfigurationModelValidator : AbstractValidator<ConsulConfigurationModel>
    {
        public ConsulConfigurationModelValidator()
        {
            RuleFor(x => x.HealthCheckEndPoint).NotEmpty();
            RuleFor(x => x.SystemAddress).NotEmpty();
            RuleFor(x => x.ServiceName).NotEmpty();
            RuleFor(x => x.ConsulHost).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}