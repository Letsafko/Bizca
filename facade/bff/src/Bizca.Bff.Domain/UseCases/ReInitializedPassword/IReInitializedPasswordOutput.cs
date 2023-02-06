namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    public interface IReInitializedPasswordOutput
    {
        void Ok(ReInitializedPasswordDto reInitializedPasswordDto);
    }
}