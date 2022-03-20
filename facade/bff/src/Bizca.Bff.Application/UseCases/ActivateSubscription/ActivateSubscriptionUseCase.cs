namespace Bizca.Bff.Application.UseCases.ActivateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Events;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Core.Application.Events;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ActivateSubscriptionUseCase : IEventHandler<ActivateSubscriptionNotification>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public ActivateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task Handle(ActivateSubscriptionNotification notification, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionByCode(notification.SubscriptionCode);
            if (subscription is null)
            {
                throw new SubscriptionDoesNotExistException($"subscription with given code {notification.SubscriptionCode} does not exist");
            }

            //subscription.SubscriptionSettings
        }
    }
}
