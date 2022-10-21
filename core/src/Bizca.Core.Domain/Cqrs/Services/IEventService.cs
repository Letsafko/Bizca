namespace Bizca.Core.Domain.Cqrs.Services
{
    using Events;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEventService
    {
        ICollection<INotificationEvent> Events { get; }
        void Enqueue(IEnumerable<INotificationEvent> events);
        Task DequeueAsync(CancellationToken cancellationToken);
    }
}