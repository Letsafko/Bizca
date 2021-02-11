namespace Bizca.Core.Infrastructure.Database.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public bool UseAzureIdentity { get; set; }
        public string ConnectionString { get; set; }
    }
}