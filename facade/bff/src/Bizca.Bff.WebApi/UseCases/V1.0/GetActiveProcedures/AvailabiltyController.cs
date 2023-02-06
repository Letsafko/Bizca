namespace Bizca.Bff.WebApi.UseCases.V1._0.GetActiveProcedures
{
    using Bizca.Bff.Application.UseCases.GetActiveProcedures;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    ///     Retrieve procedure by active subscriptions.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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