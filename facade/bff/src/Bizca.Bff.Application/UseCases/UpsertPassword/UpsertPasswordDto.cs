namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    public sealed class UpsertPasswordDto
    {
        public UpsertPasswordDto(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}