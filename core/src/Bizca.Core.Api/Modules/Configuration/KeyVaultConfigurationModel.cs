namespace Bizca.Core.Api.Modules.Configuration
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class KeyVaultConfigurationModel
    {
        [Required] public string KeyVaultName { get; set; }

        /// <summary>
        ///     Gets or sets interval between pooling changes.
        /// </summary>
        public int? ReloadInterval { get; set; }

        [NotMapped] public string KeyVaultEndpoint => $"https://{KeyVaultName}.vault.azure.net";
    }
}