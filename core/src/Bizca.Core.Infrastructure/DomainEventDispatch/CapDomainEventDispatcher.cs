namespace Bizca.Core.Infrastructure.DomainEventDispatch
{
    using Domain.Aggregate;
    using DotNetCore.CAP;
    using System.Threading;
    using System.Threading.Tasks;

    public class CapDomainEventDispatcher: IDomainEventsDispatcher
    {
        private readonly IDomainEventConsumer _domainEventConsumer;
        private readonly ITopicNameFormatter _topicNameFormatter;
        private readonly ICapPublisher _capPublisher;

        public CapDomainEventDispatcher(IDomainEventConsumer domainEventConsumer,
            ITopicNameFormatter topicNameFormatter,
            ICapPublisher capPublisher)
        {
            _domainEventConsumer = domainEventConsumer;
            _topicNameFormatter = topicNameFormatter;
            _capPublisher = capPublisher;
        }

        public async Task PublishAsync(CancellationToken cancellationToken)
        {
            foreach (var domainEvent in _domainEventConsumer.GetAllDomainEvents())
                await PublishAsync(domainEvent);
            
            _domainEventConsumer.ClearAllDomainEvents();
        }

        private async Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var topicName = _topicNameFormatter.Get(domainEvent.GetType());
            await _capPublisher.PublishAsync(topicName,
                domainEvent);
        }
    }
}