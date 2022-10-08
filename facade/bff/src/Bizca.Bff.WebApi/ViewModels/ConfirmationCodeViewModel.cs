namespace Bizca.Bff.WebApi.ViewModels
{
    /// <summary>
    ///     Confirmation code view model.
    /// </summary>
    public class ConfirmationCodeViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="ConfirmationCodeViewModel" />
        /// </summary>
        public ConfirmationCodeViewModel(string resourceId,
            string resource,
            bool confirmed)
        {
            ResourceId = resourceId;
            Confirmed = confirmed;
            Resource = resource;
        }

        /// <summary>
        ///     channel resource identifier.
        /// </summary>
        public string ResourceId { get; }

        /// <summary>
        ///     Whether is confirmed.
        /// </summary>
        public bool Confirmed { get; }

        /// <summary>
        ///     Channel resource value.
        /// </summary>
        public string Resource { get; }
    }
}