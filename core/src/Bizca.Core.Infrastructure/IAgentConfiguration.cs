namespace Bizca.Core.Infrastructure
{
    using System;
    public interface IAgentConfiguration
    {
        Uri BaseAddress { get; }
        TimeSpan? Timeout { get; }
    }
}