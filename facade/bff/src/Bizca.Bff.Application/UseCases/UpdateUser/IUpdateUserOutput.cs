namespace Bizca.Bff.Application.UseCases.UpdateUser
{
    using Bizca.Core.Domain;
    public interface IUpdateUserOutput : IPublicErrorOutput
    {
        void Ok(UpdateUserDto user);
    }
}