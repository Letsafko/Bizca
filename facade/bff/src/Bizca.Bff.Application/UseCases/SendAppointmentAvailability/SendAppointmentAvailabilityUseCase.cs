namespace Bizca.Bff.Application.UseCases.SendAppointmentAvailability
{
    using Bizca.Bff.Domain;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Events;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.Exceptions;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.EmailTemplate;
    using Bizca.Core.Domain.Services;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendAppointmentAvailabilityUseCase : ICommandHandler<SendAppointmentAvailabilityCommand>
    {
        private readonly ISendAppointmentAvailabilityOutput availabilityOutput;
        private readonly IReferentialService referentialService;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IProcedureRepository procedureRepository;
        private readonly IEventService eventService;
        public SendAppointmentAvailabilityUseCase(ISendAppointmentAvailabilityOutput availabilityOutput,
            ISubscriptionRepository subscriptionRepository,
            IReferentialService referentialService,
            IProcedureRepository procedureRepository,
            IEventService eventService)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.procedureRepository = procedureRepository;
            this.availabilityOutput = availabilityOutput;
            this.referentialService = referentialService;
            this.eventService = eventService;
        }

        public async Task<Unit> Handle(SendAppointmentAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var procedureTypeId = int.Parse(request.ProcedureId);
            var emailTemplateId = (int)EmailTemplateType.ProcedureAvailability;
            var emailTemplate = await referentialService.GetEmailTemplateByIdAsync(emailTemplateId, true);
            var procedure = await GetProcedureAsync(procedureTypeId, request.CodeInsee);
            var subscribers = await subscriptionRepository.GetSubscribers(procedure.Organism.Id, procedureTypeId);

            var events = new List<IEvent>();
            //var smsNotification = BuildSmsGroupNotification(request.PartnerCode, subscribers);
            var emailNotification = BuildEmailGroupNotification(procedure.ProcedureHref,
                procedure.ProcedureType.Label,
                emailTemplate.EmailTemplateId,
                subscribers);

            //events.Add(smsNotification);
            events.Add(emailNotification);
            eventService.Enqueue(events);

            IncrementSmsAndEmailCounter(subscribers);
            await subscriptionRepository.UpdateSubscriberAvailability(subscribers);
            availabilityOutput.Ok(true);
            return Unit.Value;
        }

        #region private helpers

        private static void IncrementSmsAndEmailCounter(IEnumerable<SubscriberAvailability> subscribers)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.IncrementEmailCounter();
                subscriber.IncrementSmsCounter();
            }
        }

        private static IEvent BuildEmailGroupNotification(string procedureHref,
            string procedureDescription,
            int emailTemplateId,
            IEnumerable<SubscriberAvailability> subscribers)
        {
            var recipients = new List<MailAddressRequest>();
            foreach (var subscriber in subscribers)
            {
                recipients.Add(new MailAddressRequest(subscriber.UserSubscription.Email));
            }

            var parameters = new Dictionary<string, object>();
            parameters.AddNewPair(AttributeConstant.Parameter.ProcedureName, procedureDescription);
            parameters.AddNewPair(AttributeConstant.Parameter.ProcedureUrl, procedureHref);
            return new SendTransactionalEmailEvent(recipients,
                parameters: parameters,
                emailTemplateId: emailTemplateId);
        }

        private static IEvent BuildSmsGroupNotification(string partnerCode, IEnumerable<SubscriberAvailability> subscribers)
        {
            return new SendTransactionalSmsEvent(partnerCode,
                recipientPhoneNumber: "",
                content: "");
        }

        private async Task<Procedure> GetProcedureAsync(int procedureTypeId, string codeInsee)
        {
            return await procedureRepository
                .GetProcedureByTypeIdAndCodeInseeAsync(procedureTypeId, codeInsee)
                ?? throw new ProcedureDoesNotExistException($"procedure related to {procedureTypeId} and {codeInsee} does not exist.");
        }

        #endregion
    }
}
