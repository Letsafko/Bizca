namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    public interface IRegisterCodeConfirmationOutput
    {
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