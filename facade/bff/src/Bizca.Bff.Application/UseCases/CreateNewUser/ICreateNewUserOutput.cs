namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Core.Domain;

    public interface ICreateNewUserOutput : IPublicErrorOutput
    {
        void Ok(CreateNewUserDto newUserDto);
    }
}