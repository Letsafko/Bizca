#nullable enable
namespace Bizca.Core.Domain
{
    public interface IPublicResponse<out T> : IPublicResponse
    {
        T? Data { get; }
    }

    public interface IPublicResponse
    {
        string? ErrorCode { get; }
        string? Message { get; }
        bool Success { get; }
        int StatusCode { get; }
    }
}