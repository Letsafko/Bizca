namespace Bizca.Bff.Application.UseCases.CreateNewContact
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Contact;
    using Bizca.Bff.Domain.Wrappers.Contact.Requests;
    using Bizca.Core.Application.Events;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateNewUserContactUseCase : IEventHandler<CreateUserContactNotification>
    {
        private readonly IContactWrapper _contactWrapper;
        public CreateNewUserContactUseCase(IContactWrapper contactWrapper)
        {
            _contactWrapper = contactWrapper;
        }

        public async Task Handle(CreateUserContactNotification notification, CancellationToken cancellationToken)
        {
            var request = new CreateUserContactRequest(notification.Email);
            request.AddContactAttributes(notification.Attributes);
            await _contactWrapper.CreateANewContactAsync(request);
        }
    }
}
