namespace Bizca.Core.Infrastructure.Database.Configuration
{
    public interface IDatabaseConfiguration
    {
        bool UseAzureIdentity { get; }
        string ConnectionString { get; }
    }
}