namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    public sealed class UpsertPasswordDto
    {
        public bool Success { get; }
        public UpsertPasswordDto(bool success)
        {
            Success = success;
        }
    }
}
