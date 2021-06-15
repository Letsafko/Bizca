namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Bundle.Exceptions;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.Exceptions;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateSubscriptionUseCase : ICommandHandler<UpdateSubscriptionCommand>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUpdateSubscriptionOutput subscriptionOutput;
        private readonly IProcedureRepository procedureRepository;
        private readonly IBundleRepository bundleRepository;
        private readonly IUserRepository userRepository;
        public UpdateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            IUpdateSubscriptionOutput subscriptionOutput,
            IProcedureRepository procedureRepository,
            IBundleRepository bundleRepository,
            IUserRepository userRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.procedureRepository = procedureRepository;
            this.subscriptionOutput = subscriptionOutput;
            this.bundleRepository = bundleRepository;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetAsync(command.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {command.ExternalUserId} does not exist.");
            }

            (Bundle bundle, Procedure procedure) = await GetEntities(command);
            if (!string.IsNullOrWhiteSpace(command.BundleId) && bundle is null)
                throw new BundleDoesNotExistException($"bundle::{command.BundleId} does not exist.");

            if (procedure is null)
                throw new ProcedureDoesNotExistException($"procedureType::{command.ProcedureTypeId} with codeInsee::{command.CodeInsee} does not exist.");

            user.UpdateSubscription(command.SubscriptionCode, bundle, procedure);
            await subscriptionRepository.UpsertAsync(user.UserIdentifier.UserId, user.Subscriptions);

            Subscription subscription = user.GetSubscriptionByCode(command.SubscriptionCode);
            subscriptionOutput.Ok(subscription);
            return Unit.Value;
        }

        #region private helpers

        private async Task<(Bundle bundle, Procedure procedure)> GetEntities(UpdateSubscriptionCommand command)
        {
            Task<Procedure> procedureTask = procedureRepository.GetProcedureByTypeIdAndCodeInseeAsync(int.Parse(command.ProcedureTypeId), command.CodeInsee);
            var bundleTask = Task.FromResult(default(Bundle));
            if (!string.IsNullOrWhiteSpace(command.BundleId))
            {
                bundleTask = bundleRepository.GetBundleByIdAsync(int.Parse(command.BundleId));
            }
            await Task.WhenAll(bundleTask, procedureTask);
            return
            (
                bundleTask.Result,
                procedureTask.Result
            );
        }

        #endregion

    }
}