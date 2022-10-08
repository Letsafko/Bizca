namespace Bizca.Core.Application.Behaviors
{
    using Commands;
    using MediatR;
    using Services;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWorkCommandBehavior<TCommand> : IPipelineBehavior<TCommand, Unit>
        where TCommand : ICommand
    {
        private readonly IEventService eventService;
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkCommandBehavior(IUnitOfWork unitOfWork, IEventService eventService)
        {
            this.eventService = eventService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken,
            RequestHandlerDelegate<Unit> next)
        {
            unitOfWork.Begin();
            try
            {
                Unit result = await next().ConfigureAwait(false);
                await eventService.DequeueAsync();
                unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}