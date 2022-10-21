#nullable enable
namespace Bizca.Core.Domain
{
    public sealed class PublicResponse<T> : IPublicResponse<T>
    {
        public PublicResponse(T? data,
            int statusCode,
            string? message = null, 
            string? errorCode = null)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Message = message;
            Data = data;
        }

        public bool Success => !string.IsNullOrWhiteSpace(ErrorCode) && !string.IsNullOrWhiteSpace(Message);
        public string? ErrorCode { get; }
        public string? Message { get; }
        public int StatusCode { get; }
        public T? Data { get; init; }
    }
}