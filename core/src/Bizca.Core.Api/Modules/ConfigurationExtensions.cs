namespace Bizca.Core.Api.Modules
{
    using Bizca.Core.Api.Modules.Configuration;
    using Microsoft.Extensions.Configuration;
    using System.ComponentModel.DataAnnotations;

    public static class ConfigurationExtension
    {
        /// <summary>
        /// Gets the versioning configuration.
        /// </summary>
        /// <param name="configuration">application configuration</param>
        public static VersioningConfigurationModel GetVersioningConfiguration(this IConfiguration configuration, string scheme = "Api:Versioning")
        {
            return configuration.GetSection(scheme).Get<VersioningConfigurationModel>();
        }

        /// <summary>
        ///     Gets or sets consul configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="configuration">application configuration</param>
        public static ConsulConfigurationModel GetConsulConfiguration(this IConfiguration configuration, string scheme = "Api:Consul")
        {
            return configuration.GetSection(scheme).Get<ConsulConfigurationModel>();
        }

        /// <summary>
        /// Gets the swagger configuration.
        /// </summary>
        /// <param name="configuration">application configuration</param>
        public static SwaggerConfigurationModel GetSwaggerConfiguration(this IConfiguration configuration, string scheme = "Api:Swagger")
        {
            SwaggerConfigurationModel model = configuration.GetSection(scheme).Get<SwaggerConfigurationModel>();
            Validator.ValidateObject(model, new ValidationContext(model));
            return model;
        }

        /// <summary>
        ///     Gets feature configuration.
        /// </summary>
        /// <param name="configuration">application configuration</param>
        public static FeaturesConfigurationModel GetFeaturesConfiguration(this IConfiguration configuration, string scheme = "Api:Features")
        {
            IConfigurationSection featuresConfiguration = configuration.GetSection(scheme);
            return featuresConfiguration.Get<FeaturesConfigurationModel>();
        }

        /// <summary>
        /// Gets the features configuration.
        /// </summary>
        /// <param name="configuration">application configuration</param>
        public static CorsConfigurationModel GetCorsConfiguration(this IConfiguration configuration, string scheme = "Api:Cors")
        {
            IConfigurationSection corsConfiguration = configuration.GetSection(scheme);

            return corsConfiguration.Get<CorsConfigurationModel>();
        }

        /// <summary>
        ///     Gets sts configuration.
        /// </summary>
        /// <param name="configuration">application configuration</param>
        public static StsConfiguration GetStsConfiguration(this IConfiguration configuration, string scheme = "Api:Sts")
        {
            return configuration.GetSection(scheme).Get<StsConfiguration>();
        }

        /// <summary>
        ///     Gets keyvault configuration.
        /// </summary>
        /// <param name="configuration">application configuration</param>
        public static KeyVaultConfigurationModel GetKeyVaultConfiguration(this IConfiguration configuration, string scheme = "Api:KeyVault")
        {
            KeyVaultConfigurationModel keyVault = configuration.GetSection(scheme).Get<KeyVaultConfigurationModel>();
            Validator.ValidateObject(keyVault, new ValidationContext(keyVault));
            return keyVault;
        }
    }
}
