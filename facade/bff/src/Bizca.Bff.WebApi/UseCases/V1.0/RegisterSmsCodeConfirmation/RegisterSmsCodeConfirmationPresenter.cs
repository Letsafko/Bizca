namespace Bizca.Bff.WebApi.UseCases.V10.RegisterSmsCodeConfirmation
{
    using Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation;
    using Bizca.Core.Api.Modules.Presentation;
    using Bizca.Core.Api.Modules.Presentation.HttpStrategies;

    /// <summary>
    ///     <see cref="RegisterSmsCodeConfirmationPresenter"/>
    /// </summary>
    public class RegisterSmsCodeConfirmationPresenter : PresenterBase,
        IRegisterSmsCodeConfirmationOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="RegisterSmsCodeConfirmationPresenter"/>
        /// </summary>
        /// <param name="strategyFactory"></param>
        public RegisterSmsCodeConfirmationPresenter(IHttpStrategyFactory strategyFactory)
            : base(strategyFactory)
        {
        }

        /// <summary>
        ///     No content
        /// </summary>
        public void Ok()
        {
            //Do nothing
        }
    }
}
