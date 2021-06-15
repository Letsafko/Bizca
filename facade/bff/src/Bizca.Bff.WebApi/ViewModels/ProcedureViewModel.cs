namespace Bizca.Bff.WebApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    internal sealed class ProcedureViewModel
    {
        public ProcedureViewModel(string procedureName,
            string procedureHref,
            string organismName,
            string codeInsee)
        {
            ProcedureName = procedureName;
            ProcedureHref = procedureHref;
            OrganismName = organismName;
            CodeInsee = codeInsee;
        }

        /// <summary>
        ///     Procedure name.
        /// </summary>
        [Required]
        public string ProcedureName { get; }

        /// <summary>
        ///     Organism name.
        /// </summary>
        [Required]
        public string OrganismName { get; }

        /// <summary>
        ///     Organism code insee.
        /// </summary>
        [Required]
        public string CodeInsee { get; }

        /// <summary>
        ///     Procedure link form.
        /// </summary>
        [Required]
        public string ProcedureHref { get; }
    }
}