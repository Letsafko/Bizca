namespace Bizca.Bff.WebApi.UseCases.V10.UpdateUser
{
    using Application.UseCases.UpdateUser;
    using Core.Api.Modules.Presentation;
    using Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Update user presenter.
    /// </summary>
    public sealed class UpdateUserPresenter : PresenterBase, IUpdateUserOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="UpdateUserPresenter" />
        /// </summary>
        /// <param name="strategyFactory"></param>
        public UpdateUserPresenter(IHttpStrategyFactory strategyFactory) : base(strategyFactory)
        {
        }

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="user"></param>
        public void Ok(UpdateUserDto user)
        {
            ViewModel = new OkObjectResult(new UserViewModel(user));
        }
    }
}