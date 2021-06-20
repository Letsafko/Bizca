namespace Bizca.Bff.Infrastructure.Wrappers.Notifications
{
    using Bizca.Core.Infrastructure;
    using System;
    public sealed class NotificationSettings : IAgentConfiguration
    {
        public TimeSpan? Timeout { get; set; }
        public Uri BaseAddress { get; set; }
    }
}
