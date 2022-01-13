namespace Bizca.Bff.WebApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Procedure type view model.
    /// </summary>
    public sealed class ProcedureLightViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="ProcedureLightViewModel"/>
        /// </summary>
        /// <param name="identifier">procedure type identifier</param>
        /// <param name="href">procedure link form.</param>
        /// <param name="settings">procedure settings</param>
        public ProcedureLightViewModel(int identifier,
            string href,
            string settings)
        {
            Identifier = identifier;
            Settings = settings;
            Href = href;
        }

        /// <summary>
        ///     Procedure link form.
        /// </summary>
        [Required]
        public string Href { get; }

        /// <summary>
        ///     Procedure settings.
        /// </summary>
        [Required]
        public string Settings { get; }

        /// <summary>
        ///     Procedure type identifier.
        /// </summary>
        [Required]
        public int Identifier { get; }
    }
}