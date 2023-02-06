namespace Bizca.Core.Api.Modules.Presentation.HttpStrategies
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public class InternalServerErrorStrategy : IHttpStrategy
    {
        public int StatusCode => (int)HttpStatusCode.InternalServerError;

        public IActionResult GetResponse(object result)
        {
            return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}