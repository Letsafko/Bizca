namespace Bizca.Core.Api.Modules.Extensions
{
    using Microsoft.Extensions.Hosting;

    public static class HostEnvironmentExtensions
    {
        private const string AzureIntegration = "AzureIntegration";
        private const string AzureQualification = "AzureQualification";

        public static bool IsDevEnvironment(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsDevelopment() || hostEnvironment.IsEnvironment(AzureIntegration) ||
                   hostEnvironment.IsEnvironment(AzureQualification);
        }
    }
}