namespace Bizca.Core.Domain
{
    public sealed class PublicResponse<T> : IPublicResponse<T>
    {
        public PublicResponse(string message, int statusCode, int? errorCode = null)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Message = message;
        }

        public PublicResponse()
        {

        }

        public bool Success => ErrorCode == null && Message == null;
        public object Message { get; }
        public int? ErrorCode { get; }
        public int StatusCode { get; }
        public T Data { get; set; }
    }
}