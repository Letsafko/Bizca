namespace Bizca.Core.Infrastructure.Configuration
{
    public sealed class BizcaDatabaseConfiguration : IDatabaseConfiguration
    {
        public bool UseAzureIdentity { get; set; }
        public string ConnectionString { get; set; }
    }
}