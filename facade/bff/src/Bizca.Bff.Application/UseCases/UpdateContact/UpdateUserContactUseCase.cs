namespace Bizca.Bff.Application.UseCases.UpdateContact
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Contact;
    using Bizca.Bff.Domain.Wrappers.Contact.Requests;
    using Bizca.Core.Application.Events;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateUserContactUseCase : IEventHandler<UpdateUserContactNotification>
    {
        private readonly IContactWrapper _contactWrapper;
        public UpdateUserContactUseCase(IContactWrapper contactWrapper)
        {
            _contactWrapper = contactWrapper;
        }

        public async Task Handle(UpdateUserContactNotification notification, CancellationToken cancellationToken)
        {
            var request = new UpdateUserContactRequest(notification.Email,
                notification.UnlinkListIds,
                notification.ListIds,
                notification.EmailBlacklisted,
                notification.SmsBlacklisted);

            request.AddContactAttributes(notification.Attributes);
            await _contactWrapper.UpdateContactAsync(notification.Email,
                request);
        }
    }
}
