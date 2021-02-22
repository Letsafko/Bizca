namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    public sealed class AuthenticateUserDto
    {
        public AuthenticateUserDto(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
    }
}
