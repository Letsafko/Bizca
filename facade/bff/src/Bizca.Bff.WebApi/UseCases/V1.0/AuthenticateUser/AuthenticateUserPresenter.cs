namespace Bizca.Bff.WebApi.UseCases.V10.AuthenticateUser
{
    using Application.UseCases.AuthenticateUser;
    using Core.Api.Modules.Presentation;
    using Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Authenticate user presenter
    /// </summary>
    public sealed class AuthenticateUserPresenter : PresenterBase, IAuthenticateUserOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="AuthenticateUserPresenter" />
        /// </summary>
        /// <param name="strategyFactory"></param>
        public AuthenticateUserPresenter(IHttpStrategyFactory strategyFactory) : base(strategyFactory)
        {
        }

        /// <summary>
        ///     Standard output.
        /// </summary>
        public void Ok(AuthenticateUserDto authenticateUser)
        {
            ViewModel = new OkObjectResult(new UserViewModel(authenticateUser));
        }
    }
}