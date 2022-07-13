using Bizca.Bff.Domain.Entities.Subscription;
namespace Bizca.Bff.Application.UseCases.PaymentSubmitted
{
    public interface IPaymentSubmittedOutput
    {
        void Ok(Subscription subscription);
    }
}
