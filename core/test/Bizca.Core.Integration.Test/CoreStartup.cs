namespace Bizca.Core.Integration.Test
{
    using Api;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class CoreStartup: StartupExtended
    {
        public CoreStartup(IConfiguration configuration, IHostEnvironment environment) 
            : base(configuration, environment)
        {
        }
    }
}