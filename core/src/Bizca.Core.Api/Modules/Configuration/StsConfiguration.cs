namespace Bizca.Core.Api.Modules.Configuration
{
    using System;

    public class StsConfiguration
    {
        public string Provider { get; set; }
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public bool EnableCaching { get; set; }
        public TimeSpan CacheDuration { get; set; }
    }
}