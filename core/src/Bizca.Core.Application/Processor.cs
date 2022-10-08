namespace Bizca.Core.Application
{
    using Commands;
    using Domain;
    using MediatR;
    using Queries;
    using System.Threading.Tasks;

    public sealed class Processor : IProcessor
    {
        public async Task ProcessCommandAsync(ICommand command)
        {
            await _mediator.Send(command).ConfigureAwait(false);
        }

        public async Task<TResult> ProcessCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await _mediator.Send(command).ConfigureAwait(false);
        }

        public async Task ProcessQueryAsync(IQuery query)
        {
            await _mediator.Send(query).ConfigureAwait(false);
        }

        public async Task<TResult> ProcessQueryAsync<TResult>(IQuery<TResult> query)
        {
            return await _mediator.Send(query).ConfigureAwait(false);
        }

        public async Task ProcessNotificationAsync(IEvent @event)
        {
            await _mediator.Publish(@event).ConfigureAwait(false);
        }

        #region fields & ctor

        private readonly IMediator _mediator;

        public Processor(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion
    }
}