namespace Bizca.Bff.Application.UseCases.UpdateUser
{
    public interface IUpdateUserOutput
    {
        void Ok(UpdateUserDto user);
    }
}