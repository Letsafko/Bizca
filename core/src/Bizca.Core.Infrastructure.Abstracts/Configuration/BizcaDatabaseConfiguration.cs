namespace Bizca.Core.Infrastructure.Abstracts.Configuration
{
    public sealed class BizcaDatabaseConfiguration : IDatabaseConfiguration
    {
        public bool UseAzureIdentity { get; set; }
        public string ConnectionString { get; set; }
    }
}
