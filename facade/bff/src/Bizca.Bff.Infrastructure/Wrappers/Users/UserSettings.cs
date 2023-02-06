namespace Bizca.Bff.Infrastructure.Wrappers.Users
{
    using Core.Infrastructure;
    using System;

    public sealed class UserSettings : IAgentConfiguration
    {
        public TimeSpan? Timeout { get; set; }
        public Uri BaseAddress { get; set; }
    }
}