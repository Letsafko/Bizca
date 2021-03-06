using Bizca.User.Application.UseCases.RegisterPassword;

namespace Bizca.User.WebApi.UseCases.V1.RegisterPassword
{
    /// <summary>
    ///     Register password response.
    /// </summary>
    public sealed class RegisterPasswordResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="RegisterPasswordResponse"/>
        /// </summary>
        /// <param name="registerPasswordDto"></param>
        public RegisterPasswordResponse(RegisterPasswordDto registerPasswordDto)
        {
            Success = registerPasswordDto.Success;
        }

        /// <summary>
        ///     Indicates whether password has been created succesfully.
        /// </summary>
        public bool Success { get; }
    }
}