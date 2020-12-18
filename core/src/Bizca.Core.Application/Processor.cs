namespace Bizca.Core.Application
{
    using Bizca.Core.Application.Abstracts;
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Application.Abstracts.Queries;
    using MediatR;
    using System;
    using System.Threading.Tasks;

    public sealed class Processor : IProcessor
    {
        #region fields & ctor

        private readonly IMediator _mediator;
        public Processor(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        public async Task ProcessCommandAsync(ICommand command)
        {
            await _mediator.Send(command).ConfigureAwait(false);
        }
        public async Task ProcessCommandAsync<TResult>(ICommand<TResult> command)
        {
            await _mediator.Send(command).ConfigureAwait(false);
        }

        public async Task ProcessQueryAsync(IQuery query)
        {
            await _mediator.Send(query).ConfigureAwait(false);
        }
        public async Task ProcessQueryAsync<TResult>(IQuery<TResult> query)
        {
            await _mediator.Send(query).ConfigureAwait(false);
        }

        public async Task ProcessNotificationAsync(INotification notification)
        {
            await _mediator.Publish(notification).ConfigureAwait(false);
        }
    }
}
