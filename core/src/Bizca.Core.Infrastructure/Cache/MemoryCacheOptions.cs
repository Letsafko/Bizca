namespace Bizca.Core.Infrastructure.Cache
{
    public sealed class MemoryCacheOptions
    {
        public int DurationInMinutes { get; set; } = 24 * 60;
    }
}