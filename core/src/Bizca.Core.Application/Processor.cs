﻿namespace Bizca.Core.Application
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Queries;
    using Bizca.Core.Domain;
    using MediatR;
    using System.Threading.Tasks;

    public sealed class Processor : IProcessor
    {
        #region fields & ctor

        private readonly IMediator _mediator;
        public Processor(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

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
    }
}