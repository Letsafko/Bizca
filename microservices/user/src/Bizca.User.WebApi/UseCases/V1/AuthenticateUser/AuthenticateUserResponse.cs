namespace Bizca.User.WebApi.UseCases.V1.AuthenticateUser
{
    using Domain.Agregates;
    using System.ComponentModel.DataAnnotations;
    using ViewModels;

    /// <summary>
    ///     Update password response.
    /// </summary>
    public sealed class AuthenticateUserResponse : UserModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="AuthenticateUserResponse" />
        /// </summary>
        /// <param name="user"></param>
        public AuthenticateUserResponse(User user) : base(user)
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