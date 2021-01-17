namespace Bizca.Core.Api.Modules.Extensions
{
    using Bizca.Core.Api.Modules.Configuration;
    using Microsoft.Azure.KeyVault;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureKeyVault;
    using System;

    /// <summary>
    /// ConfigurationBuilderExtension class
    /// </summary>
    public static class ConfigurationBuilderExtension
    {
        private const string KEYVAULT_NAME_ENV_VARIABLE_NAME = "KEYVAULT_NAME";

        public static IConfigurationBuilder AddAzureKeyVault(this IConfigurationBuilder builder, KeyVaultConfigurationModel keyVaultConfiguration)
        {
            return builder.AddAzureKeyVault(keyVaultConfiguration, new DefaultKeyVaultSecretManager());
        }


        /// <summary>Adds the azure key vault with the env variables.</summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAzureKeyVaultWithEnvVariables(this IConfigurationBuilder builder)
        {
            string keyVaultName = Environment.GetEnvironmentVariable(KEYVAULT_NAME_ENV_VARIABLE_NAME);

            return string.IsNullOrEmpty(keyVaultName)
                ? builder
                : builder.AddAzureKeyVault(new KeyVaultConfigurationModel() { KeyVaultName = keyVaultName }, new DefaultKeyVaultSecretManager());
        }

        /// <summary>
        /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from the Azure KeyVault.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="keyVaultConfiguration">The key vault configuration.</param>
        /// <param name="secretManager">The secret manager.</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAzureKeyVault(this IConfigurationBuilder builder, KeyVaultConfigurationModel keyVaultConfiguration, IKeyVaultSecretManager secretManager)
        {
            var tokenProvider = new AzureServiceTokenProvider();

            var authenticationCallback = new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback);

            var keyVaultClient = new KeyVaultClient(authenticationCallback);

            TimeSpan? reloadInterval = null;

            if (keyVaultConfiguration.ReloadInterval.HasValue)
            {
                reloadInterval = TimeSpan.FromMilliseconds(keyVaultConfiguration.ReloadInterval.Value);
            }

            builder.AddAzureKeyVault(new AzureKeyVaultConfigurationOptions
            {
                Client = keyVaultClient,
                ReloadInterval = reloadInterval,
                Vault = keyVaultConfiguration.KeyVaultEndpoint,
                Manager = secretManager
            });

            return builder;
        }
    }
}