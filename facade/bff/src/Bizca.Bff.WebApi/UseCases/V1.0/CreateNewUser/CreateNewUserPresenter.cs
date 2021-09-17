namespace Bizca.Bff.WebApi.UseCases.V10.CreateNewUser
{
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Presentation;
    using Bizca.Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Create new user Presenter.
    /// </summary>
    public sealed class CreateNewUserPresenter : PresenterBase, ICreateNewUserOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="CreateNewUserPresenter"/>
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