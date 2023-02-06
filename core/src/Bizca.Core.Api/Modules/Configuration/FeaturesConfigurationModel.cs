namespace Bizca.Core.Api.Modules.Configuration
{
    public class FeaturesConfigurationModel
    {
        public bool ApplicationInsights { get; set; }
        
        public bool Versioning { get; set; }
        
        
        public bool KeyVault { get; set; }

        public bool Logging { get; set; }

        public bool Swagger { get; set; }

        public bool Consul { get; set; }

        public bool Cors { get; set; }

        public bool Sts { get; set; }
    }
}