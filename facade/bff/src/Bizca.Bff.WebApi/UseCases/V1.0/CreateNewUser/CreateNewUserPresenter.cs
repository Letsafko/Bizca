namespace Bizca.Bff.WebApi.UseCases.V10.CreateNewUser
{
    using Application.UseCases.CreateNewUser;
    using Core.Api.Modules.Presentation;
    using Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Create new user Presenter.
    /// </summary>
    public sealed class CreateNewUserPresenter : PresenterBase, ICreateNewUserOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="CreateNewUserPresenter" />
        /// </summary>
        /// <param name="strategyFactory"></param>
        public CreateNewUserPresenter(IHttpStrategyFactory strategyFactory) : base(strategyFactory)
        {
        }

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="newUserDto"></param>
        public void Ok(CreateNewUserDto newUserDto)
        {
            ViewModel = new CreatedResult(string.Empty, new UserViewModel(newUserDto));
        }
    }
}