namespace Bizca.Bff.Application.UseCases.UpdateContact
{
    using Core.Application.Events;
    using Domain.Events;
    using Domain.Wrappers.Contact;
    using Domain.Wrappers.Contact.Requests;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateUserContactUseCase : IEventHandler<UserContactUpdatedEvent>
    {
        private readonly IContactWrapper _contactWrapper;

        public UpdateUserContactUseCase(IContactWrapper contactWrapper)
        {
            _contactWrapper = contactWrapper;
        }

        public async Task Handle(UserContactUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var request = new UpdateUserContactRequest(notification.Email,
                attributes: notification.Attributes);

            await _contactWrapper.UpdateContactAsync(notification.Email,
                request);
        }
    }
}