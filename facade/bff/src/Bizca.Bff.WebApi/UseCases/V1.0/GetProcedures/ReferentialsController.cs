namespace Bizca.Bff.WebApi.UseCases.V10.GetProcedures
{
    using Application.UseCases.GetProcedures;
    using Core.Api.Modules.Conventions;
    using Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Creates referential controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class ReferentialsController : ControllerBase
    {
        private readonly GetProceduresPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="ReferentialsController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public ReferentialsController(GetProceduresPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Get procedure list.
        /// </summary>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpGet("procedures")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganismCollectionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> GetProceduresAsync()
        {
            await processor.ProcessQueryAsync(new GetProceduresQuery()).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}