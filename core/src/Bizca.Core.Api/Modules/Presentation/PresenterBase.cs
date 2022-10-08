﻿namespace Bizca.Core.Api.Modules.Presentation
{
    using Domain;
    using HttpStrategies;
    using Microsoft.AspNetCore.Mvc;

    public abstract class PresenterBase
    {
        private readonly IHttpStrategyFactory strategyFactory;

        protected PresenterBase(IHttpStrategyFactory strategyFactory)
        {
            this.strategyFactory = strategyFactory;
        }

        public IActionResult ViewModel { get; protected set; } = new NoContentResult();

        public void Invalid(IPublicResponse response)
        {
            ViewModel = GetActionResult(response);
        }

        private IActionResult GetActionResult(IPublicResponse response)
        {
            IHttpStrategy strategy = strategyFactory.GetStrategy(response.StatusCode);
            return strategy.GetResponse(response.Message);
        }
    }
}