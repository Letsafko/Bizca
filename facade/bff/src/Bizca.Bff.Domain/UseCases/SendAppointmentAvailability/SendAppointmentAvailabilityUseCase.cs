namespace Bizca.Bff.Application.UseCases.SendAppointmentAvailability
{
    using Core.Domain;
    using Core.Domain.Cqrs.Commands;
    using Core.Domain.Cqrs.Events;
    using Core.Domain.Cqrs.Services;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using Domain;
    using Domain.Entities.Subscription;
    using Domain.Events;
    using Domain.Referential.Procedure;
    using Domain.Referential.Procedure.Exceptions;
    using Domain.Wrappers.Notification.Requests.Email;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendAppointmentAvailabilityUseCase : ICommandHandler<SendAppointmentAvailabilityCommand>
    {
        private readonly ISendAppointmentAvailabilityOutput availabilityOutput;
        private readonly IEventService eventService;
        private readonly IProcedureRepository procedureRepository;
        private readonly IReferentialService referentialService;
        private readonly ISubscriptionRepository subscriptionRepository;

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
            int procedureTypeId = int.Parse(request.ProcedureId);
            int emailTemplateId = (int)EmailTemplateType.ProcedureAvailability;
            EmailTemplate emailTemplate = await referentialService.GetEmailTemplateByIdAsync(emailTemplateId, true);
            Procedure procedure = await GetProcedureAsync(procedureTypeId, request.CodeInsee);
            IEnumerable<SubscriberAvailability> subscribers =
                await subscriptionRepository.GetSubscribers(procedure.Organism.Id, procedureTypeId);

            var events = new List<INotificationEvent>();
            //var smsNotification = BuildSmsGroupNotification(request.PartnerCode, subscribers);
            INotificationEvent emailNotification = BuildEmailGroupNotification(procedure.ProcedureHref,
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
            foreach (SubscriberAvailability subscriber in subscribers)
            {
                subscriber.IncrementEmailCounter();
                subscriber.IncrementSmsCounter();
            }
        }

        private static INotificationEvent BuildEmailGroupNotification(string procedureHref,
            string procedureDescription,
            int emailTemplateId,
            IEnumerable<SubscriberAvailability> subscribers)
        {
            var recipients = new List<MailAddressRequest>();
            foreach (SubscriberAvailability subscriber in subscribers)
                recipients.Add(new MailAddressRequest(subscriber.UserSubscription.Email));

            var parameters = new Dictionary<string, object>();
            parameters.AddNewPair(AttributeConstant.Parameter.ProcedureName, procedureDescription);
            parameters.AddNewPair(AttributeConstant.Parameter.ProcedureUrl, procedureHref);
            return new SendTransactionalEmailNotificationEvent(recipients,
                parameters: parameters,
                emailTemplateId: emailTemplateId);
        }

        private static INotificationEvent BuildSmsGroupNotification(string partnerCode,
            IEnumerable<SubscriberAvailability> subscribers)
        {
            return new SendTransactionalSmsNotificationEvent(partnerCode,
                "",
                "");
        }

        private async Task<Procedure> GetProcedureAsync(int procedureTypeId, string codeInsee)
        {
            return await procedureRepository
                       .GetProcedureByTypeIdAndCodeInseeAsync(procedureTypeId, codeInsee)
                   ?? throw new ProcedureDoesNotExistException(
                       $"procedure related to {procedureTypeId} and {codeInsee} does not exist.");
        }

        #endregion
    }
}