namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Core.Domain;
    public interface ICreateNewUserOutput : IPublicErrorOutput
    {
        void Ok(CreateNewUserDto newUserDto);
    }
}