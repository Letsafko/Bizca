namespace Bizca.Core.Infrastructure.DomainEventDispatch
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainEventsDispatcher
    {
        Task PublishAsync(CancellationToken cancellationToken);
    }
}