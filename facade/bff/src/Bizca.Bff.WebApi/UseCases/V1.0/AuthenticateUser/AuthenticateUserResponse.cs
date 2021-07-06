namespace Bizca.Bff.WebApi.UseCases.V10.AuthenticateUser
{
    using Bizca.Bff.Application.UseCases.AuthenticateUser;
    using Bizca.Bff.WebApi.ViewModels;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Authenticate user response
    /// </summary>
    internal sealed class AuthenticateUserResponse : UserViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="AuthenticateUserResponse"/>
        /// </summary>
        /// <param name="userDto"></param>
        public AuthenticateUserResponse(AuthenticateUserDto userDto) : base(userDto)
        {
            Authenticated = true;
        }

        /// <summary>
        ///     Indicates whether user has been authenticated succesfully.
        /// </summary>
        [Required]
        public bool Authenticated { get; }
    }
}