namespace Bizca.Bff.Application.UseCases.ActivateUserContact
{
    using Bizca.Bff.Domain;
    using Bizca.Bff.Domain.Events;
    using Bizca.Bff.Domain.Provider.ContactList;
    using Bizca.Bff.Domain.Provider.Folder;
    using Bizca.Bff.Domain.Wrappers.Contact;
    using Bizca.Bff.Domain.Wrappers.Contact.Requests;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Services;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ActivateUserContactUseCase : IEventHandler<ActivateUserContactEvent>
    {
        private readonly IReferentialService _referentialService;
        private readonly IFolderRepository _folderRepository;
        private readonly IContactListRepository _contactListRepository;
        private readonly IContactWrapper _contactWrapper;
        public ActivateUserContactUseCase(IContactListRepository contactListRepository,
            IReferentialService referentialService,
            IFolderRepository folderRepository,
            IContactWrapper contactWrapper)
        {
            _contactListRepository = contactListRepository;
            _referentialService = referentialService;
            _folderRepository = folderRepository;
            _contactWrapper = contactWrapper;
        }

        public async Task Handle(ActivateUserContactEvent notification, CancellationToken cancellationToken)
        {
            var contactList = await _contactListRepository.GetContactListByProcedureAndOrganismAsync(notification.Procedure.ProcedureType.Id,
                                    notification.Procedure.Organism.Id);

            if (contactList is null)
            {
                var partner = await _referentialService.GetPartnerByCodeAsync(notification.PartnerCode, true);
                var folder = await _folderRepository.GetFolderAsync(partner.Id);

                var listName = $"{notification.Procedure.Organism.CodeInsee}#{notification.Procedure.ProcedureType.Id}";
                var listId = await _contactWrapper.CreateANewListAsync(new UserContactListRequest(listName, folder.FolderId));

                contactList = new ContactList
                {
                    ProcedureTypeId = notification.Procedure.ProcedureType.Id,
                    OrganismId = notification.Procedure.Organism.Id,
                    ListId = listId.Data.Id,
                    Name = listName
                };

                await _contactListRepository.AddNewContactList(contactList);
            }

            var request = new UpdateUserContactRequest(notification.Email,
                listIds: new List<int> { contactList.ListId },
                attributes: new Dictionary<string, object>
                {
                    [AttributeConstant.Contact.Email] = notification.Email
                });

            await _contactWrapper.UpdateContactAsync(notification.Email,
                request);
        }
    }
}
