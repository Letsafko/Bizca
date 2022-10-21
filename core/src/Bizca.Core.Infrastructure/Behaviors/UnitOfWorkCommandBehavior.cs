namespace Bizca.Core.Infrastructure.Behaviors
{
    using Database;
    using Domain.Cqrs.Commands;
    using Domain.Cqrs.Services;
    using Extension;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWorkCommandBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, Unit>
        where TCommand : ICommand, IRequest<TResponse>
    {
        private readonly IEventService _eventService;
        private readonly ILogger<UnitOfWorkCommandBehavior<TCommand, TResponse>> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandBehavior(IUnitOfWork unitOfWork,
            IEventService eventService,
            ILogger<UnitOfWorkCommandBehavior<TCommand, TResponse>> logger)
        {
            _eventService = eventService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(TCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
        {
            _unitOfWork.Begin();
            try
            {
                Unit result = await next();
                await _eventService.DequeueAsync(cancellationToken);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "an error occured while processing command {CommandName}",
                    request.GetGenericTypeName());

                _unitOfWork.Rollback();

                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}