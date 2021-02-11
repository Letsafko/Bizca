namespace Bizca.Core.Domain
{
    public sealed class Response<T>
    {
        public T Value { get; }
        public bool Success => ModelState.IsValid;
        public Notification ModelState { get; } = new Notification();

        public Response(T value)
        {
            Value = value;
        }
    }
}