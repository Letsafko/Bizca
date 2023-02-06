namespace Bizca.Core.Domain.Aggregate
{
    using System.Collections.Generic;

    public interface IDomainEventConsumer
    {
        IReadOnlyList<IDomainEvent> GetAllDomainEvents();
        void Consume(IEnumerable<IDomainEvent> domainEvents);
        void  ClearAllDomainEvents();
    }
}