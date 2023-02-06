namespace Bizca.User.Application.UseCases.ConfirmChannelCode;

public interface IConfirmChannelCodeOutput
{
    /// <summary>
    ///     Invalid data.
    /// </summary>
    /// <param name="message"></param>
    void Invalid(string message);

    /// <summary>
    ///     Not found.
    /// </summary>
    void NotFound(string message);

    /// <summary>
    ///     Ok code confirmation.
    /// </summary>
    /// <param name="codeConfirmation">code confirmation.</param>
    void Ok(ConfirmChannelCodeDto codeConfirmation);
}