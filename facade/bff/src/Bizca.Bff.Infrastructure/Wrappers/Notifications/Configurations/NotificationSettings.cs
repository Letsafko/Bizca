namespace Bizca.Bff.Infrastructure.Wrappers.Notifications.Configurations
{
    using Core.Infrastructure;
    using System;

    public record NotificationSettings(TimeSpan? Timeout, Uri BaseAddress) : IAgentConfiguration;
}