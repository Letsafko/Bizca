namespace Bizca.Core.Infrastructure.Configurations
{
    using Bizca.Core.Infrastructure.Abstracts.Configuration;

    public sealed class UserManagmentDatabase : IDatabaseConfiguration
    {
        public bool UseAzureIdentity { get; set; }
        public string ConnectionString { get; set; }
    }
}
