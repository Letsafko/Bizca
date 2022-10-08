namespace Bizca.Bff.WebApi.UseCases.V10.UpsertPassword
{
    using Application.UseCases.UpsertPassword;
    using Core.Api.Modules.Presentation;
    using Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

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