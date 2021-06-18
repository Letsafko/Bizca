namespace Bizca.Bff.WebApi.ViewModels
{
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    internal sealed class BundleViewModel
    {
        public BundleViewModel(Bundle bundle)
        {
            TotalEmail = bundle.BundleSettings.TotalEmail;
            TotalSms = bundle.BundleSettings.TotalSms;
            BundleId = bundle.BundleIdentifier.Id;
            Name = bundle.BundleIdentifier.Label;
            Code = bundle.BundleIdentifier.Code;
            Amount = bundle.Price.Amount;
        }

        /// <summary>
        ///     Bundle identifier.
        /// </summary>
        [Required]
        [JsonProperty("identifier")]
        public int BundleId { get; }

        /// <summary>
        ///     Bundle description.
        /// </summary>
        [Required]
        [JsonProperty("description")]
        public string Name { get; }

        /// <summary>
        ///     Bundle code.
        /// </summary>
        [Required]
        [JsonProperty("code")]
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