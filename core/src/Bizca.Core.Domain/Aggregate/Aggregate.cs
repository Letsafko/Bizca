namespace Bizca.Core.Domain.Aggregate
{
    using System.Collections.Generic;

    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents;

        protected AggregateRoot()
        {
            _domainEvents = new List<IDomainEvent>();
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}