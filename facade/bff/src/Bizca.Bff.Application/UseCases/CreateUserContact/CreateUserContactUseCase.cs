namespace Bizca.Bff.Application.UseCases.CreateUserContact
{
    using Core.Application.Events;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using Domain.Entities.User.Exceptions;
    using Domain.Events;
    using Domain.Provider.Folder;
    using Domain.Wrappers.Contact;
    using Domain.Wrappers.Contact.Requests;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateUserContactUseCase : IEventHandler<UserContactToCreateEvent>
    {
        private readonly IContactWrapper _contactWrapper;
        private readonly IFolderRepository _folderRepository;
        private readonly IReferentialService _referentialService;

        public CreateUserContactUseCase(IContactWrapper contactWrapper,
            IReferentialService referentialService,
            IFolderRepository folderRepository)
        {
            _referentialService = referentialService;
            _folderRepository = folderRepository;
            _contactWrapper = contactWrapper;
        }

        public async Task Handle(UserContactToCreateEvent notification, CancellationToken cancellationToken)
        {
            Partner partner = await _referentialService.GetPartnerByCodeAsync(notification.PartnerCode, true);
            Folder folder = await GetFolderAsync(partner.Id);

            var request = new CreateUserContactRequest(notification.Email,
                new HashSet<int> { folder.ListId },
                attributes: notification.Attributes);

            await _contactWrapper.CreateANewContactAsync(request);
        }

        private async Task<Folder> GetFolderAsync(int partnerId)
        {
            return await _folderRepository.GetFolderAsync(partnerId)
                   ?? throw new FolderDoesNotExistException($"folder for partner {partnerId} does not exist.");
        }
    }
}