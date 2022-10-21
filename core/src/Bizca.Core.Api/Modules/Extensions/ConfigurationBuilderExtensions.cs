namespace Bizca.Core.Api.Modules.Extensions
{
    using Configuration;
    using Microsoft.Azure.KeyVault;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureKeyVault;
    using System;

    public static class ConfigurationBuilderExtension
    {
        private const string KeyVaultNameEnvVariableName = "KEYVAULT_NAME";

        public static IConfigurationBuilder AddAzureKeyVault(this IConfigurationBuilder builder,
            KeyVaultConfigurationModel keyVaultConfiguration)
        {
            return builder.AddAzureKeyVault(keyVaultConfiguration, new DefaultKeyVaultSecretManager());
        }

        public static IConfigurationBuilder AddAzureKeyVaultWithEnvVariables(this IConfigurationBuilder builder)
        {
            string keyVaultName = Environment.GetEnvironmentVariable(KeyVaultNameEnvVariableName);
            return string.IsNullOrWhiteSpace(keyVaultName)
                ? builder
                : builder.AddAzureKeyVault(new KeyVaultConfigurationModel
                    {
                        KeyVaultName = keyVaultName
                    },
                    new DefaultKeyVaultSecretManager());
        }

        private static IConfigurationBuilder AddAzureKeyVault(this IConfigurationBuilder builder,
            KeyVaultConfigurationModel keyVaultConfiguration, 
            IKeyVaultSecretManager secretManager)
        {
            var tokenProvider = new AzureServiceTokenProvider();
            var authenticationCallback = new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback);
            var keyVaultClient = new KeyVaultClient(authenticationCallback);

            TimeSpan? reloadInterval = null;
            if (keyVaultConfiguration.ReloadInterval.HasValue)
                reloadInterval = TimeSpan.FromMilliseconds(keyVaultConfiguration.ReloadInterval.Value);

            builder.AddAzureKeyVault(new AzureKeyVaultConfigurationOptions
            {
                Vault = keyVaultConfiguration.KeyVaultEndpoint,
                ReloadInterval = reloadInterval,
                Client = keyVaultClient,
                Manager = secretManager
            });

            return builder;
        }
    }
}