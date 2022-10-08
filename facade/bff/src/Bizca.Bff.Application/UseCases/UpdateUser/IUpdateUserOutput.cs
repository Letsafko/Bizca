namespace Bizca.Bff.Application.UseCases.UpdateUser
{
    using Core.Domain;

    public interface IUpdateUserOutput : IPublicErrorOutput
    {
        void Ok(UpdateUserDto user);
    }
}