namespace Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword
{
    public sealed class CreateOrUpdateUserPasswordDto
    {
        public CreateOrUpdateUserPasswordDto(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}