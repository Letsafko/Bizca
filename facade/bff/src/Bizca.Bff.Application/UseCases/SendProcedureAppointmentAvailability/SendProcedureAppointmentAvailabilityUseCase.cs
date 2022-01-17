namespace Bizca.Bff.Application.UseCases.SendProcedureAppointmentAvailability
{
    using Bizca.Bff.Application.Properties;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.EmailTemplate;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendProcedureAppointmentAvailabilityUseCase : ICommandHandler<SendProcedureAppointmentAvailabilityCommand>
    {
        private readonly ISendProcedureAppointmentAvailabilityOutput availabilityOutput;
        private readonly IEmailTemplateRepository emailTemplateRepository;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IEventService eventService;
        public SendProcedureAppointmentAvailabilityUseCase(ISendProcedureAppointmentAvailabilityOutput availabilityOutput,
            ISubscriptionRepository subscriptionRepository,
            IEmailTemplateRepository emailTemplateRepository,
            IEventService eventService)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.availabilityOutput = availabilityOutput;
            this.emailTemplateRepository = emailTemplateRepository;
            this.eventService = eventService;
        }

        public async Task<Unit> Handle(SendProcedureAppointmentAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var emailTemplate = await emailTemplateRepository.GetByIdAsync((int)EmailTemplateType.ProcedureAvailability);
            var subscribers = await subscriptionRepository.GetSubscribers(int.Parse(request.OrganismId),
                int.Parse(request.ProcedureId));

            var events = new List<IEvent>();
            var smsNotification = BuildSmsGroupNotification(request.PartnerCode, subscribers);
            var emailNotification = BuildEmailGroupNotification(request.PartnerCode,
                request.ProcedureHref,
                emailTemplate.EmailTemplateId,
                subscribers);

            events.Add(smsNotification);
            events.Add(emailNotification);
            //eventService.Enqueue(events);

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

        private static IEvent BuildEmailGroupNotification(string partnerCode,
            string procedureHref,
            int emailTemplateId,
            IEnumerable<SubscriberAvailability> subscribers)
        {
            var recipients = new List<MailAddressRequest>();
            var sender = new MailAddressRequest(partnerCode, Resources.BIZCA_NO_REPLY_EMAIL);
            foreach (var subscriber in subscribers)
            {
                string fullName = $"{subscriber.UserSubscription.FirstName} {subscriber.UserSubscription.LastName}";
                recipients.Add(new MailAddressRequest(fullName, subscriber.UserSubscription.Email));
            }

            var notification = new SendEmailNotification(sender, recipients);
            notification.AddNewParam("PROCEDURE_HREF", procedureHref);
            notification.SetTemplate(emailTemplateId);
            return notification;
        }

        private static IEvent BuildSmsGroupNotification(string partnerCode, IEnumerable<SubscriberAvailability> subscribers)
        {
            return new SendSmsNotification(partnerCode,
                recipientPhoneNumber: "",
                content: "");
        }

        #endregion
    }
}
