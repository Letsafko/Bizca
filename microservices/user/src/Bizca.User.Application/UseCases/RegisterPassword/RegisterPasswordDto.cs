namespace Bizca.User.Application.UseCases.RegisterPassword
{
    public sealed class RegisterPasswordDto
    {
        public RegisterPasswordDto(bool succes)
        {
            Success = succes;
        }
        public bool Success { get; }
    }
}