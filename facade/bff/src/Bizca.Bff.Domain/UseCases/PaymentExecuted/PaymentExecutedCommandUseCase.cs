namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    using Core.Domain.Cqrs.Commands;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class PaymentExecutedCommandUseCase : ICommandHandler<PaymentExecutedCommand>
    {
        public async Task<Unit> Handle(PaymentExecutedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}