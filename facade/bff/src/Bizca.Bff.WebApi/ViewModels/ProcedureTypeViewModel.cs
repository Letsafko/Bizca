using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Bizca.Bff.WebApi.ViewModels
{
    /// <summary>
    ///     Procedure type view model.
    /// </summary>
    public sealed class ProcedureTypeViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="ProcedureTypeViewModel"/>
        /// </summary>
        /// <param name="procedureTypeId">procedure type identifier</param>
        /// <param name="procedureHref">procedure link form.</param>
        /// <param name="procedureTypeLabel">procedure type label</param>
        public ProcedureTypeViewModel(int procedureTypeId,
            string procedureHref,
            string procedureTypeLabel)
        {
            ProcedureTypeLabel = procedureTypeLabel;
            ProcedureTypeId = procedureTypeId;
            ProcedureHref = procedureHref;
        }

        /// <summary>
        ///     Procedure type label.
        /// </summary>
        [Required]
        [JsonProperty("description")]
        public string ProcedureTypeLabel { get; }

        /// <summary>
        ///     Procedure link form.
        /// </summary>
        [Required]
        [JsonProperty("href")]
        public string ProcedureHref { get; }

        /// <summary>
        ///     Procedure type identifier.
        /// </summary>
        [Required]
        [JsonProperty("identifier")]
        public int ProcedureTypeId { get; }
    }
}