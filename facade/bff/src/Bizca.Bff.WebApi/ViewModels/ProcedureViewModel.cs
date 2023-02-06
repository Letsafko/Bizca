namespace Bizca.Bff.WebApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    internal sealed class ProcedureViewModel
    {
        public ProcedureViewModel(string organismName,
            string codeInsee,
            ProcedureTypeViewModel procedureType)
        {
            ProcedureType = procedureType;
            OrganismName = organismName;
            CodeInsee = codeInsee;
        }

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
        ///     Procedure type.
        /// </summary>
        [Required]
        public ProcedureTypeViewModel ProcedureType { get; }
    }
}