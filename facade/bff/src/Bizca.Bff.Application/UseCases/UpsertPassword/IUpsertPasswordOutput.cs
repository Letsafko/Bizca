namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    public interface IUpsertPasswordOutput
    {
        void Ok(UpsertPasswordDto passwordDto);
    }
}
