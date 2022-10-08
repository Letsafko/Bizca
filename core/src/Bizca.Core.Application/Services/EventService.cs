namespace Bizca.Core.Application.Services
{
    using Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class EventService : IEventService
    {
        private readonly IProcessor processor;

        public EventService(IProcessor processor)
        {
            Events = new List<IEvent>();
            this.processor = processor;
        }

        public ICollection<IEvent> Events { get; }

        public async Task DequeueAsync()
        {
            foreach (IEvent @event in Events) await processor.ProcessNotificationAsync(@event);
        }

        public void Enqueue(IEnumerable<IEvent> events)
        {
            if (events?.Any() != true) return;
            foreach (IEvent @event in events) Events.Add(@event);
        }
    }
}