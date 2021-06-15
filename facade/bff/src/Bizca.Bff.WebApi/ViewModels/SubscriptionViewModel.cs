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
                int remaingEmail = subscription.SubscriptionSettings.TotalEmail - subscription.SubscriptionSettings.EmailCounter;
                int remaingSms = subscription.SubscriptionSettings.TotalSms - subscription.SubscriptionSettings.SmsCounter;
                Bundle = new BundleViewModel(subscription.Bundle.BundleIdentifier.Label,
                    subscription.Price.Amount,
                    remaingSms,
                    remaingEmail);

                BeginDate = subscription.SubscriptionSettings.BeginDate;
                EndDate = subscription.SubscriptionSettings.EndDate;
            }

            Procedure = new ProcedureViewModel(subscription.Procedure.ProcedureType.Label,
                subscription.Procedure.ProcedureHref,
                subscription.Procedure.Organism.OrganismName,
                subscription.Procedure.Organism.CodeInsee);

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
        ///     Bundle.
        /// </summary>
        public BundleViewModel Bundle { get; }

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
