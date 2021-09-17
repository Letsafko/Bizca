namespace Bizca.Core.Domain
{
    public sealed class Response<T>
    {
        public T Data { get; }
        public int? ErrorCode { get; }
        public int? StatusCode { get; }
        public string ErrorMessage { get; }
        public Response(T data, string errorMessage, int? errorCode, int? statusCode)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Data = data;
        }
    }
}