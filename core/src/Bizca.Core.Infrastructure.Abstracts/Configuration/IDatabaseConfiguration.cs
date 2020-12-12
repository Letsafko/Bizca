namespace Bizca.Core.Infrastructure.Abstracts.Configuration
{
    public interface IDatabaseConfiguration
    {
        bool UseAzureIdentity { get; }
        string ConnectionString { get; }
    }
}
