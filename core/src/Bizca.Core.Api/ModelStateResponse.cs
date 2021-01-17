namespace Bizca.Core.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public sealed class ModelStateResponse
    {
        public ModelStateResponse(int status, IEnumerable<string> errors, object stackstrace = null)
        {
            Status = status;
            Errors = errors;
            StackTrace = stackstrace;
        }

        [JsonProperty("status")]
        public int Status { get; }

        [JsonProperty("errors")]
        public IEnumerable<string> Errors { get; }

        [JsonProperty("stacktrace", NullValueHandling = NullValueHandling.Ignore)]
        public object StackTrace { get; }
    }
}