namespace Bizca.Core.Api.Modules.Presentation.HttpStrategies
{
    using Microsoft.AspNetCore.Mvc;
    public interface IHttpStrategy
    {
        public int StatusCode { get; }
        public IActionResult GetResponse(object message);
    }
}