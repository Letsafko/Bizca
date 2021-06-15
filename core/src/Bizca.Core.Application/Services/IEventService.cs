namespace Bizca.Core.Application.Services
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventService
    {
        ICollection<IEvent> Events { get; }
        void Enqueue(IEnumerable<IEvent> events);
        Task DequeueAsync();
    }
}