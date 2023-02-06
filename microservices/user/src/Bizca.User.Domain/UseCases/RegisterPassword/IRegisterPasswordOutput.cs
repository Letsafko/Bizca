namespace Bizca.User.Application.UseCases.RegisterPassword;

public interface IRegisterPasswordOutput
{
    void NotFound(string message);
    void Ok(RegisterPasswordDto passwordCreated);
}