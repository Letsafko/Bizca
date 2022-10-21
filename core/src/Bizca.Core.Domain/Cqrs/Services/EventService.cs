namespace Bizca.Core.Domain.Cqrs.Services
{
    using Events;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class EventService : IEventService
    {
        private readonly IProcessor _processor;

        public EventService(IProcessor processor)
        {
            Events = new List<INotificationEvent>();
            _processor = processor;
        }

        public ICollection<INotificationEvent> Events { get; }

        public async Task DequeueAsync(CancellationToken cancellationToken)
        {
            foreach (INotificationEvent @event in Events) 
                await _processor.ProcessNotificationAsync(@event, cancellationToken);
        }

        public void Enqueue(IEnumerable<INotificationEvent> events)
        {
            foreach (INotificationEvent @event in events) 
                Events.Add(@event);
        }
    }
}