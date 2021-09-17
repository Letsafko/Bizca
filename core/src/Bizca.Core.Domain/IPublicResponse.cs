namespace Bizca.Core.Domain
{
    public interface IPublicResponse<T> : IPublicResponse
    {
        public T Data { get; }
    }

    public interface IPublicResponse
    {
        public bool Success { get; }
        public object Message { get; }
        public int? ErrorCode { get; }
        public int StatusCode { get; }
    }
}