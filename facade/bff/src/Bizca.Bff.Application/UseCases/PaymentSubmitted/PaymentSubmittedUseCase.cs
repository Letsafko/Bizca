namespace Bizca.Bff.Application.UseCases.PaymentSubmitted
{
    using Core.Application.Commands;
    using Domain.Entities.Subscription;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using Domain.Referentials.Bundle;
    using Domain.Referentials.Bundle.Exceptions;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class PaymentSubmittedUseCase : ICommandHandler<PaymentSubmittedCommand>
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IPaymentSubmittedOutput _subscriptionPaymentOutput;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;

        public PaymentSubmittedUseCase(IUserRepository userRepository,
            ISubscriptionRepository subscriptionRepository,
            IBundleRepository bundleRepository,
            IPaymentSubmittedOutput subscriptionPaymentOutput)
        {
            _subscriptionPaymentOutput = subscriptionPaymentOutput;
            _subscriptionRepository = subscriptionRepository;
            _bundleRepository = bundleRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(PaymentSubmittedCommand request, CancellationToken cancellationToken)
        {
            Bundle bundle = await _bundleRepository.GetBundleByIdAsync(int.Parse(request.BundleId));
            if (bundle is null)
                throw new BundleDoesNotExistException($"bundle identifier {request.BundleId} does not exist.");

            User user = await _userRepository.GetByExternalUserIdAsync(request.ExternalUserId);
            if (user is null) throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");

            user.UpdateSubscriptionBundle(request.SubscriptionCode, bundle);
            await _subscriptionRepository.UpsertAsync(user.Id, user.Subscriptions);

            //ToDo: add agent to submit payment
            _subscriptionPaymentOutput.Ok(user.GetSubscriptionByCode(request.SubscriptionCode));
            return Unit.Value;
        }
    }
}