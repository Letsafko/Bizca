namespace Bizca.Gateway.Application.Configuration
{
    using Core.Api.Modules.Configuration;

    public class ApiFeaturesConfigurationModel : FeaturesConfigurationModel
    {
        public bool Caching { get; set; }
    }
}