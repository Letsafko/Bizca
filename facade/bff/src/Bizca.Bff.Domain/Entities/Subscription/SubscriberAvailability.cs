namespace Bizca.Bff.Domain.Entities.Subscription
{
    public sealed class SubscriberAvailability
    {
        public SubscriberAvailability(UserSubscription userSubscription,
            int subscriptionId,
            int emailCounter,
            int smsCounter)
        {
            UserSubscription = userSubscription;
            SubscriptionId = subscriptionId;
            EmailCounter = emailCounter;
            SmsCounter = smsCounter;
        }

        public UserSubscription UserSubscription { get; }
        public int EmailCounter { get; private set; }
        public int SmsCounter { get; private set; }
        public int SubscriptionId { get; }

        public void IncrementEmailCounter()
        {
            EmailCounter++;
        }

        public void IncrementSmsCounter()
        {
            SmsCounter++;
        }
    }
}