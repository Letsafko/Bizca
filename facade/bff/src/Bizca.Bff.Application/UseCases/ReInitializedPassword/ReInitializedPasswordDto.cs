namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    public sealed class ReInitializedPasswordDto
    {
        public ReInitializedPasswordDto(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}
