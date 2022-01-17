namespace Bizca.Bff.WebApi.UseCases.V10.GetProceduresForActiveSubscriptions
{
    using Bizca.Bff.Application.UseCases.GetProceduresForActiveSubscriptions;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.Core.Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    ///     Retrieve procedure by active subscriptions.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class AvailabiltyController : ControllerBase
    {
        private readonly GetProceduresForActiveSubscriptionsPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="AvailabiltyController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public AvailabiltyController(GetProceduresForActiveSubscriptionsPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Retrieve procedure by active subscriptions.
        /// </summary>
        /// <remarks>/Assets/getProceduresByActiveSubscriptions.md</remarks>
        [HttpGet("procedures")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProcedureLightCollectionViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IPublicResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
        public async Task<IActionResult> GetProceduresForActiveSubscriptionsAsync()
        {
            await processor.ProcessQueryAsync(new GetProceduresForActiveSubscriptionsQuery()).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}