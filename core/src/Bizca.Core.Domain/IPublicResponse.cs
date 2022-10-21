#nullable enable
namespace Bizca.Core.Domain
{
    public interface IPublicResponse<out T> : IPublicResponse
    {
        public T? Data { get; }
    }

    public interface IPublicResponse
    {
        int StatusCode { get; }
        public bool Success { get; }
        public string? Message { get; }
        public string? ErrorCode { get; }
    }
}