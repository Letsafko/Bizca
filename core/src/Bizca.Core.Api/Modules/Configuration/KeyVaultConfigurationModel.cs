namespace Bizca.Core.Api.Modules.Configuration
{
    using FluentValidation;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class KeyVaultConfigurationModel
    {
        [Required] public string KeyVaultName { get; set; }

        public int? ReloadInterval { get; set; }

        [NotMapped] public string KeyVaultEndpoint => $"https://{KeyVaultName}.vault.azure.net";
    }
    
    public class KeyVaultConfigurationModelValidator : AbstractValidator<KeyVaultConfigurationModel>
    {
        public KeyVaultConfigurationModelValidator()
        {
            RuleFor(x => x.KeyVaultName).NotEmpty();
        }
    }
}