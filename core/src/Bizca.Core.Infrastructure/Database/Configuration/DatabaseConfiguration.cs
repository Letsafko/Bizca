namespace Bizca.Core.Infrastructure.Database.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public bool UseAzureIdentity { get; init; }
        public string ConnectionString { get; init; }
    }
}