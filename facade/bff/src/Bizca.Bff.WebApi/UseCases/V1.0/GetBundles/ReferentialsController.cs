namespace Bizca.Bff.WebApi.UseCases.V1._0.GetBundles
{
    using Bizca.Bff.Application.UseCases.GetBundles;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates referential controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class ReferentialsController : ControllerBase
    {
        private readonly GetBundlesPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="ReferentialsController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public ReferentialsController(GetBundlesPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Get bundle list.
        /// </summary>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpGet("bundles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BundleCollectionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> GetBundlesAsync()
        {
            await processor.ProcessQueryAsync(new GetBundlesQuery()).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}