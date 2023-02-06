namespace Bizca.Bff.WebApi.UseCases.V1._0.AuthenticateUser
{
    using Bizca.Bff.Application.UseCases.AuthenticateUser;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Presentation;
    using Bizca.Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;

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