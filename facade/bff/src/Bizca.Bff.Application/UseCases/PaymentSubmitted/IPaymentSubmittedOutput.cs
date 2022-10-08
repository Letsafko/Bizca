namespace Bizca.Bff.Application.UseCases.PaymentSubmitted
{
    using Domain.Entities.Subscription;

    public interface IPaymentSubmittedOutput
    {
        void Ok(Subscription subscription);
    }
}