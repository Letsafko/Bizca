namespace Bizca.Bff.WebApi.UseCases.V1._0.UpsertPassword
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Register or update user password
    /// </summary>
    public sealed class UpsertPassword
    {
        /// <summary>
        ///     Password to hash.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Channel resource value.
        /// </summary>
        [Required]
        public string Resource { get; set; }
    }
}