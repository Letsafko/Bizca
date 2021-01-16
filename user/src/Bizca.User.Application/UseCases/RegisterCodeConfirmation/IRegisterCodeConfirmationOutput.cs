namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.Core.Domain;

    public interface IRegisterCodeConfirmationOutput
    {
        /// <summary>
        ///     Invalid input.
        /// </summary>
        /// <param name="notification"></param>
        void Invalid(Notification notification);

        /// <summary>
        ///     User not found.
        /// </summary>
        void NotFound(string message);

        /// <summary>
        ///     Creates an user.
        /// </summary>
        /// <param name="codeConfirmation">code confirmation.</param>
        void Ok(RegisterCodeConfirmationDto codeConfirmation);
    }
}
