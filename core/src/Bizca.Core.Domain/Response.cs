namespace Bizca.Core.Domain
{
    public sealed class Response<T>
    {
        public T Value { get; }
        public bool Success { get; }
        public string Message { get; }

        public Response(T value, bool success, string message)
        {
            Value = value;
            Success = success;
            Message = message;
        }
    }
}
