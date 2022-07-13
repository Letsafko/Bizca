namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
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
        private readonly IUserRepository userRepository;
        public UpdateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            IUpdateSubscriptionOutput subscriptionOutput,
            IProcedureRepository procedureRepository,
            IUserRepository userRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.procedureRepository = procedureRepository;
            this.subscriptionOutput = subscriptionOutput;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(command.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {command.ExternalUserId} does not exist.");
            }

            var procedure = await GetProcedureAsync(command);
            user.UpdateSubscription(command.SubscriptionCode, procedure);

            await subscriptionRepository.UpsertAsync(user.UserIdentifier.UserId, user.Subscriptions);
            subscriptionOutput.Ok(user.GetSubscriptionByCode(command.SubscriptionCode));
            return Unit.Value;
        }

        private async Task<Procedure> GetProcedureAsync(UpdateSubscriptionCommand command)
        {
            return await procedureRepository.GetProcedureByTypeIdAndCodeInseeAsync(int.Parse(command.ProcedureTypeId),
                command.CodeInsee)
                ?? throw new ProcedureDoesNotExistException($"procedureType::{command.ProcedureTypeId} with codeInsee::{command.CodeInsee} does not exist.");
        }
    }
}