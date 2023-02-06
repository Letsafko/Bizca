namespace Bizca.Core.Api.Modules.Presentation.HttpStrategies
{
    public interface IHttpStrategyFactory
    {
        public IHttpStrategy GetStrategy(int statusCode);
    }
}