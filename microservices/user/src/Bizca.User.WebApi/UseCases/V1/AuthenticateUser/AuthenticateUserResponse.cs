namespace Bizca.User.WebApi.UseCases.V1.AuthenticateUser
{
    using Bizca.User.Application.UseCases.AuthenticateUser;

    /// <summary>
    ///     Update password response.
    /// </summary>
    public sealed class AuthenticateUserResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="AuthenticateUserResponse"/>
        /// </summary>
        /// <param name="AuthenticateUserDto"></param>
        public AuthenticateUserResponse(AuthenticateUserDto AuthenticateUserDto)
        {
            Success = AuthenticateUserDto.Success;
        }

        /// <summary>
        ///     Indicates whether password has been updated succesfully.
        /// </summary>
        public bool Success { get; }
    }
}
