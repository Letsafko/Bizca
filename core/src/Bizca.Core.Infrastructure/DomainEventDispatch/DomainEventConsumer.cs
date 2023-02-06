namespace Bizca.Core.Infrastructure.DomainEventDispatch
{
    using Domain.Aggregate;
    using System.Collections.Generic;

    public class DomainEventConsumer: IDomainEventConsumer
    {
        public DomainEventConsumer()
        {
            _domainEvents = new List<IDomainEvent>();
        }

        public void Consume(IEnumerable<IDomainEvent> domainEvents)
        {
            _domainEvents.AddRange(domainEvents);
        }

        private readonly List<IDomainEvent> _domainEvents;
        public IReadOnlyList<IDomainEvent> GetAllDomainEvents()
        {
            return _domainEvents;
        }

        public void ClearAllDomainEvents()
        {
            _domainEvents.Clear();
        }

    
    }
}