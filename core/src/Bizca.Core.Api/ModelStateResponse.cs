namespace Bizca.Core.Api
{
    using Newtonsoft.Json;
    public sealed class ModelStateResponse
    {
        public ModelStateResponse(int status, string errorMessage, object stackstrace = null)
        {
            ErrorMessage = errorMessage;
            StackTrace = stackstrace;
            Status = status;
        }

        [JsonProperty("stacktrace", NullValueHandling = NullValueHandling.Ignore)]
        public object StackTrace { get; }

        [JsonProperty("error")]
        public string ErrorMessage { get; }

        [JsonProperty("status")]
        public int Status { get; }
    }
}