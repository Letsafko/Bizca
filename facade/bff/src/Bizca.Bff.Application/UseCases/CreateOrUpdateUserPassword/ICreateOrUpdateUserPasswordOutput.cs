namespace Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword
{
    public interface ICreateOrUpdateUserPasswordOutput
    {
        public void Ok(CreateOrUpdateUserPasswordDto createOrUpdateUserPasswordDto);
    }
}