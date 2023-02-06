namespace Bizca.Core.Api.Modules.Presentation.HttpStrategies
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public class NotFoundStrategy : IHttpStrategy
    {
        public int StatusCode => (int)HttpStatusCode.NotFound;

        public IActionResult GetResponse(object result)
        {
            return new NotFoundObjectResult(result);
        }
    }
}