namespace Bizca.Core.Api
{
    using Newtonsoft.Json;
    public sealed class ModelStateResponse
    {
        public ModelStateResponse(int status, string errorMessage)
        {
            ErrorMessage = errorMessage;
            ErrorCode = status;
        }

        [JsonProperty("error")]
        public object ErrorMessage { get; }

        [JsonProperty("code")]
        public int ErrorCode { get; }
    }
}