namespace Bizca.Core.Infrastructure.Behaviors
{
    using Database;
    using Domain.Cqrs.Commands;
    using DomainEventDispatch;
    using Extension;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWorkCommandBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, Unit>
        where TCommand : ICommand, IRequest<TResponse>
    {
        private readonly ILogger<UnitOfWorkCommandBehavior<TCommand, TResponse>> _logger;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandBehavior(IUnitOfWork unitOfWork,
            ILogger<UnitOfWorkCommandBehavior<TCommand, TResponse>> logger, 
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _domainEventsDispatcher = domainEventsDispatcher;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(TCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
        {
            _unitOfWork.Begin();
            try
            {
                Unit result = await next();
            
                _unitOfWork.Commit();
            
                await _domainEventsDispatcher.PublishAsync(cancellationToken);
            
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