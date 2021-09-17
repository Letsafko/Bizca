namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    using Bizca.Core.Domain;
    public interface IUpsertPasswordOutput : IPublicErrorOutput
    {
        void Ok(UpsertPasswordDto passwordDto);
    }
}
