namespace Bizca.Core.Api.Modules.Presentation.HttpStrategies
{
    using System.Collections.Generic;
    using System.Linq;
    public sealed class HttpStrategyFactory : IHttpStrategyFactory
    {
        private readonly IEnumerable<IHttpStrategy> strategies;
        public HttpStrategyFactory(IEnumerable<IHttpStrategy> strategies)
        {
            this.strategies = strategies;
        }

        public IHttpStrategy GetStrategy(int statusCode)
        {
            return strategies.Single(x => x.StatusCode == statusCode);
        }
    }
}