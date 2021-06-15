namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    public interface ICreateNewUserOutput
    {
        void Ok(CreateNewUserDto newUserDto);
    }
}