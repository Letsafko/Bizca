namespace Bizca.Bff.Application.UseCases.CreateNewContact
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Provider.Folder;
    using Bizca.Bff.Domain.Wrappers.Contact;
    using Bizca.Bff.Domain.Wrappers.Contact.Requests;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Services;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateNewUserContactUseCase : IEventHandler<CreateUserContactNotification>
    {
        private readonly IContactWrapper _contactWrapper;
        private readonly IReferentialService _referentialService;
        private readonly IFolderRepository _folderRepository;
        public CreateNewUserContactUseCase(IContactWrapper contactWrapper,
            IReferentialService referentialService,
            IFolderRepository folderRepository)
        {
            _referentialService = referentialService;
            _folderRepository = folderRepository;
            _contactWrapper = contactWrapper;
        }

        public async Task Handle(CreateUserContactNotification notification, CancellationToken cancellationToken)
        {
            var partner = await _referentialService.GetPartnerByCodeAsync(notification.PartnerCode, true);
            var folder = await GetFolderAsync(partner.Id);

            var request = new CreateUserContactRequest(notification.Email, new HashSet<int> { folder.ListId });
            request.AddContactAttributes(notification.Attributes);
            await _contactWrapper.CreateANewContactAsync(request);
        }

        private async Task<Folder> GetFolderAsync(int partnerId)
        {
            return await _folderRepository.GetFolderAsync(partnerId)
                ?? throw new FolderDoesNotExistException($"folder for partner {partnerId} does not exist.");
        }
    }
}
