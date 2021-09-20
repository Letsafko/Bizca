namespace Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation
{
    using Bizca.Core.Domain;
    public interface IRegisterSmsCodeConfirmationOutput
    {
        void Invalid(IPublicResponse response);
        void Ok();
    }
}