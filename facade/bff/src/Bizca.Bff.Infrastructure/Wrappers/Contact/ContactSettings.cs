namespace Bizca.Bff.Infrastructure.Wrappers.Contact
{
    using Bizca.Core.Infrastructure;
    using System;

    public sealed class ContactSettings : IAgentConfiguration
    {
        public TimeSpan? Timeout { get; set; }
        public Uri BaseAddress { get; set; }
    }
}
