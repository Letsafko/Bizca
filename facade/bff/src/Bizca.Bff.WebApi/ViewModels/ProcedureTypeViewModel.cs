namespace Bizca.Bff.WebApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Procedure type view model.
    /// </summary>
    public sealed class ProcedureTypeViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="ProcedureTypeViewModel" />
        /// </summary>
        /// <param name="idenitifier">procedure type identifier</param>
        /// <param name="href">procedure link form.</param>
        /// <param name="description">procedure type label</param>
        public ProcedureTypeViewModel(int idenitifier,
            string href,
            string description)
        {
            Description = description;
            Identifier = idenitifier;
            Href = href;
        }

        /// <summary>
        ///     Procedure type label.
        /// </summary>
        [Required]
        public string Description { get; }

        /// <summary>
        ///     Procedure link form.
        /// </summary>
        [Required]
        public string Href { get; }

        /// <summary>
        ///     Procedure type identifier.
        /// </summary>
        [Required]
        public int Identifier { get; }
    }
}