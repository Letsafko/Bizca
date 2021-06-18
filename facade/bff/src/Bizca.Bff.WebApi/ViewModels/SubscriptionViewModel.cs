namespace Bizca.Bff.WebApi.ViewModels
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using System;
    using System.ComponentModel.DataAnnotations;

    internal class SubscriptionViewModel
    {
        public SubscriptionViewModel(Subscription subscription)
        {
            if (subscription.Bundle != null && subscription.SubscriptionSettings != null)
            {
                Bundle = subscription.Bundle.BundleIdentifier.Code;
                Amount = subscription.Bundle.Price.Amount;
                RemainingEmail = subscription.SubscriptionSettings.TotalEmail - subscription.SubscriptionSettings.EmailCounter;
                RemainingSms = subscription.SubscriptionSettings.TotalSms - subscription.SubscriptionSettings.SmsCounter;
                BeginDate = subscription.SubscriptionSettings.BeginDate;
                EndDate = subscription.SubscriptionSettings.EndDate;
            }

            Procedure = new ProcedureViewModel(subscription.Procedure.Organism.OrganismName,
                subscription.Procedure.Organism.CodeInsee,
                new ProcedureTypeViewModel(subscription.Procedure.ProcedureType.Id,
                subscription.Procedure.ProcedureHref,
                subscription.Procedure.ProcedureType.Label));

            Status = subscription.SubscriptionState.Status.ToString();
            Reference = subscription.SubscriptionCode.ToString();
        }

        /// <summary>
        ///     Subscription status.
        /// </summary>
        [Required]
        public string Status { get; }

        /// <summary>
        ///     Subscription reference.
        /// </summary>
        [Required]
        public string Reference { get; }

        /// <summary>
        ///     Bundle amount
        /// </summary>
        public decimal? Amount { get; }

        /// <summary>
        ///     Bundle name
        /// </summary>
        public string Bundle { get; }

        /// <summary>
        ///     Remaing email balance.
        /// </summary>
        public int? RemainingEmail { get; }

        /// <summary>
        ///     Remaining sms balance.
        /// </summary>
        public int? RemainingSms { get; }

        /// <summary>
        ///     Procedure.
        /// </summary>
        [Required]
        public ProcedureViewModel Procedure { get; }

        /// <summary>
        ///     Subscription start date.
        /// </summary>
        public DateTime? BeginDate { get; }

        /// <summary>
        ///     Subscription end date.
        /// </summary>
        public DateTime? EndDate { get; }
    }
}