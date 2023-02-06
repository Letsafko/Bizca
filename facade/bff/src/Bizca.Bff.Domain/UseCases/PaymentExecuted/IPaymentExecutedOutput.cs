namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    using Domain.Entities.Subscription;

    public interface IPaymentExecutedOutput
    {
        void Ok(Subscription subscription);
    }
}