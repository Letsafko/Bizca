namespace Bizca.Bff.WebApi.UseCases.V10.GetActiveProcedures
{
    using Application.UseCases.GetActiveProcedures;
    using Core.Api.Modules.Conventions;
    using Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Retrieve procedure by active subscriptions.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class AvailabiltyController : ControllerBase
    {
        private readonly GetActiveProceduresPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="AvailabiltyController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public AvailabiltyController(GetActiveProceduresPresenter presenter, IProcessor processor)
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
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> GetProceduresForActiveSubscriptionsAsync()
        {
            await processor.ProcessQueryAsync(new GetActiveProceduresQuery()).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}