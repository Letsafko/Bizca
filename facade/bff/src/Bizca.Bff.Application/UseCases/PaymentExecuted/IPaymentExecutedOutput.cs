using Bizca.Bff.Domain.Entities.Subscription;

namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    public interface IPaymentExecutedOutput
    {
        void Ok(Subscription subscription);
    }
}
