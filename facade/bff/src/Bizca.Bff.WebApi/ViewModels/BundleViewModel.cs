namespace Bizca.Bff.WebApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    internal sealed class BundleViewModel
    {
        public BundleViewModel(string name,
            decimal amount, 
            int remainingSms,
            int remainingEmail)
        {
            RemainingEmail = remainingEmail;
            RemainingSms = remainingSms;
            Amount = amount;
            Name = name;
        }

        /// <summary>
        ///     Bundle amount
        /// </summary>
        [Required]
        public decimal Amount { get; }

        /// <summary>
        ///     Bundle name
        /// </summary>
        [Required]
        public string Name { get; }

        /// <summary>
        ///     Remaing email balance.
        /// </summary>
        [Required]
        public int RemainingEmail { get; }

        /// <summary>
        ///     Remaining sms balance.
        /// </summary>
        [Required]
        public int RemainingSms { get; }
    }
}
