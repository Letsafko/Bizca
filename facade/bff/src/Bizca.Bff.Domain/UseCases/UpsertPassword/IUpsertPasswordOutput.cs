namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    using Core.Domain;

    public interface IUpsertPasswordOutput : IPublicErrorOutput
    {
        void Ok(UpsertPasswordDto passwordDto);
    }
}