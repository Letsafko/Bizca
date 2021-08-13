namespace Bizca.Core.Infrastructure.Configuration
{
    using Bizca.Core.Infrastructure.Database.Configuration;
    public sealed class BizcaDatabaseConfiguration : IDatabaseConfiguration
    {
        public bool UseAzureIdentity { get; set; }
        public string ConnectionString { get; set; }
    }
}