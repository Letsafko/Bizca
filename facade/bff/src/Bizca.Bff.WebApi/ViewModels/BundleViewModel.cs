namespace Bizca.Bff.WebApi.ViewModels
{
    using Domain.Referential.Bundle;
    using System.ComponentModel.DataAnnotations;

    internal sealed class BundleViewModel
    {
        public BundleViewModel(Bundle bundle)
        {
            TotalEmail = bundle.BundleSettings.TotalEmail;
            TotalSms = bundle.BundleSettings.TotalSms;
            Identifier = bundle.BundleIdentifier.Id;
            Description = bundle.BundleIdentifier.Label;
            Code = bundle.BundleIdentifier.Code;
            Amount = bundle.Price.Amount;
        }

        /// <summary>
        ///     Bundle identifier.
        /// </summary>
        [Required]
        public int Identifier { get; }

        /// <summary>
        ///     Bundle description.
        /// </summary>
        [Required]
        public string Description { get; }

        /// <summary>
        ///     Bundle code.
        /// </summary>
        [Required]
        public string Code { get; }

        /// <summary>
        ///     Bundle amount
        /// </summary>
        [Required]
        public decimal Amount { get; }

        /// <summary>
        ///     Total email balance.
        /// </summary>
        [Required]
        public int TotalEmail { get; }

        /// <summary>
        ///     Total sms balance.
        /// </summary>
        [Required]
        public int TotalSms { get; }
    }
}