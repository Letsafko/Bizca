namespace Bizca.Core.Domain.Cqrs
{
    using Commands;
    using Events;
    using MediatR;
    using Queries;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class Processor : IProcessor
    {
        private readonly IMediator _mediator;

        public Processor(IMediator mediator)
        {
            _mediator = mediator;
        }
    
        public async Task ProcessCommandAsync(ICommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        public async Task ProcessQueryAsync(IQuery query, CancellationToken cancellationToken)
        {
            await _mediator.Send(query, cancellationToken);
        }

        public async Task ProcessNotificationAsync(INotificationEvent notificationEvent,
            CancellationToken cancellationToken)
        {
            await _mediator.Publish(notificationEvent, cancellationToken);
        }
    }
}