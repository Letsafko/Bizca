namespace Bizca.Bff.WebApi.UseCases.V1._0.UpsertPassword
{
    using Bizca.Bff.Application.UseCases.UpsertPassword;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Presentation;
    using Bizca.Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Register or update user password presenter.
    /// </summary>
    public sealed class UpsertPasswordPresenter : PresenterBase, IUpsertPasswordOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="UpsertPasswordPresenter" />
        /// </summary>
        /// <param name="strategyFactory"></param>
        public UpsertPasswordPresenter(IHttpStrategyFactory strategyFactory) : base(strategyFactory)
        {
        }

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="password">user password dto</param>
        public void Ok(UpsertPasswordDto password)
        {
            ViewModel = new OkObjectResult(new UserPasswordViewModel(password.Success));
        }
    }
}