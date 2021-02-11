namespace Bizca.Core.Application.Behaviors
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWorkCommandBehavior<TCommand> : IPipelineBehavior<TCommand, Unit>
        where TCommand : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            _unitOfWork.Begin();
            try
            {
                Unit result = await next().ConfigureAwait(false);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}