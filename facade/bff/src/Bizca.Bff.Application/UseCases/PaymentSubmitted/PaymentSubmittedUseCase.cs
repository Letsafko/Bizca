namespace Bizca.Bff.Application.UseCases.PaymentSubmitted
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Bundle.Exceptions;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class PaymentSubmittedUseCase : ICommandHandler<PaymentSubmittedCommand>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPaymentSubmittedOutput _subscriptionPaymentOutput;
        private readonly IBundleRepository _bundleRepository;
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
            var bundle = await _bundleRepository.GetBundleByIdAsync(int.Parse(request.BundleId));
            if (bundle is null)
            {
                throw new BundleDoesNotExistException($"bundle identifier {request.BundleId} does not exist.");
            }

            var user = await _userRepository.GetByExternalUserIdAsync(request.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");
            }

            user.UpdateSubscriptionBundle(request.SubscriptionCode, bundle);
            await _subscriptionRepository.UpsertAsync(user.Id, user.Subscriptions);

            //ToDo: add agent to submit payment
            _subscriptionPaymentOutput.Ok(user.GetSubscriptionByCode(request.SubscriptionCode));
            return Unit.Value;
        }
    }
}