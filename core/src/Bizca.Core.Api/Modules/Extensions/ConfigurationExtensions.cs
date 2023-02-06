namespace Bizca.Core.Api.Modules.Extensions
{
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using System.ComponentModel.DataAnnotations;

    public static class ConfigurationExtension
    {
        public static VersioningConfigurationModel GetVersioningConfiguration(this IConfiguration configuration,
            string scheme = "Api:Versioning")
        {
            return configuration.GetSection(scheme).Get<VersioningConfigurationModel>();
        }

        public static ConsulConfigurationModel GetConsulConfiguration(this IConfiguration configuration,
            string scheme = "Api:Consul")
        {
            return configuration.GetSection(scheme).Get<ConsulConfigurationModel>();
        }

        public static SwaggerConfigurationModel GetSwaggerConfiguration(this IConfiguration configuration,
            string scheme = "Api:Swagger")
        {
            var model = configuration.GetSection(scheme).Get<SwaggerConfigurationModel>();
            Validator.ValidateObject(model, new ValidationContext(model));
            return model;
        }

        public static FeaturesConfigurationModel GetFeaturesConfiguration(this IConfiguration configuration,
            string scheme = "Api:Features")
        {
            IConfigurationSection featuresConfiguration = configuration.GetSection(scheme);
            return featuresConfiguration.Get<FeaturesConfigurationModel>();
        }

        public static CorsConfigurationModel GetCorsConfiguration(this IConfiguration configuration,
            string scheme = "Api:Cors")
        {
            IConfigurationSection corsConfiguration = configuration.GetSection(scheme);

            return corsConfiguration.Get<CorsConfigurationModel>();
        }

        public static StsConfiguration GetStsConfiguration(this IConfiguration configuration, 
            string scheme = "Api:Sts")
        {
            return configuration.GetSection(scheme).Get<StsConfiguration>();
        }

        public static KeyVaultConfigurationModel GetKeyVaultConfiguration(this IConfiguration configuration,
            string scheme = "Api:KeyVault")
        {
            var keyVault = configuration.GetSection(scheme).Get<KeyVaultConfigurationModel>();
            Validator.ValidateObject(keyVault, new ValidationContext(keyVault));
            return keyVault;
        }

        public static T GetConfiguration<T>(this IConfiguration configuration, string scheme = "Api:Features")
            where T : new()
        {
            IConfigurationSection featuresConfiguration = configuration.GetSection(scheme);
            return featuresConfiguration.Get<T>() ?? new T();
        }

        public static ApplicationInsightsConfigurationModel GetApplicationInsightsConfiguration(
            this IConfiguration configuration, string scheme = "Api:ApplicationInsights")
        {
            return configuration.GetSection(scheme).Get<ApplicationInsightsConfigurationModel>();
        }
    }
}