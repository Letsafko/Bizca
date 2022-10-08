namespace Bizca.Bff.Infrastructure.Wrappers.Notifications.Configurations
{
    using Core.Infrastructure;
    using System;

    public sealed class NotificationSettings : IAgentConfiguration
    {
        public TimeSpan? Timeout { get; set; }
        public Uri BaseAddress { get; set; }
    }
}