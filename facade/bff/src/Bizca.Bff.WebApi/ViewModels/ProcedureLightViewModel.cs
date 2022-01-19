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
        /// <param name="procedureId">procedure type identifier</param>
        /// <param name="codeInsee">organism code identifier</param>
        /// <param name="procedureHref">procedure link form.</param>
        /// <param name="settings">procedure settings</param>
        public ProcedureLightViewModel(int procedureId,
            string codeInsee,
            string procedureHref,
            string settings)
        {
            ProcedureId = procedureId;
            CodeInsee = codeInsee;
            Settings = settings;
            ProcedureHref = procedureHref;
        }

        /// <summary>
        ///     Procedure link form.
        /// </summary>
        [Required]
        public string ProcedureHref { get; }

        /// <summary>
        ///     Procedure settings.
        /// </summary>
        [Required]
        public string Settings { get; }

        /// <summary>
        ///     Procedure type identifier.
        /// </summary>
        [Required]
        public int ProcedureId { get; }

        /// <summary>
        ///     Organism code identifier.
        /// </summary>
        [Required]
        public string CodeInsee { get; }
    }
}