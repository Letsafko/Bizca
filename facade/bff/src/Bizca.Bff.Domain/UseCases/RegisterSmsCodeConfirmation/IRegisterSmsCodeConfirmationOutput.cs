namespace Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation
{
    using Core.Domain;

    public interface IRegisterSmsCodeConfirmationOutput
    {
        void Invalid(IPublicResponse response);
        void Ok();
    }
}