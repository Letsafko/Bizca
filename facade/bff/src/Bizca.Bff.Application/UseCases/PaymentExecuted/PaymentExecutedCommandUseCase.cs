namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class PaymentExecutedCommandUseCase : ICommandHandler<PaymentExecutedCommand>
    {
        public PaymentExecutedCommandUseCase()
        {
        }

        public async Task<Unit> Handle(PaymentExecutedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
