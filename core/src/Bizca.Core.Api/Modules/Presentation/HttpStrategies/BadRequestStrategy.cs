namespace Bizca.Core.Api.Modules.Presentation.HttpStrategies
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    public class BadRequestStrategy : IHttpStrategy
    {
        public int StatusCode => (int)HttpStatusCode.BadRequest;
        public IActionResult GetResponse(object message)
        {
            return new BadRequestObjectResult(message);
        }
    }
}
