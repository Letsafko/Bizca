namespace Bizca.Bff.Application.UseCases.ActivateUserContact
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using Domain;
    using Domain.Events;
    using Domain.Provider.ContactList;
    using Domain.Provider.Folder;
    using Domain.Wrappers.Contact;
    using Domain.Wrappers.Contact.Requests;
    using Domain.Wrappers.Contact.Responses;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ActivateUserContactUseCase : IEventHandler<ActivateUserContactNotificationEvent>
    {
        private readonly IContactListRepository _contactListRepository;
        private readonly IContactWrapper _contactWrapper;
        private readonly IFolderRepository _folderRepository;
        private readonly IReferentialService _referentialService;

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

        public async Task Handle(ActivateUserContactNotificationEvent notification, CancellationToken cancellationToken)
        {
            ContactList contactList = await _contactListRepository.GetContactListByProcedureAndOrganismAsync(
                notification.Procedure.ProcedureType.Id,
                notification.Procedure.Organism.Id);

            if (contactList is null)
            {
                Partner partner = await _referentialService.GetPartnerByCodeAsync(notification.PartnerCode, true);
                Folder folder = await _folderRepository.GetFolderAsync(partner.Id);

                string listName =
                    $"{notification.Procedure.Organism.CodeInsee}#{notification.Procedure.ProcedureType.Id}";
                IPublicResponse<ContactListResponse> listId =
                    await _contactWrapper.CreateANewListAsync(new UserContactListRequest(listName, folder.FolderId));

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
                attributes: new Dictionary<string, object> { [AttributeConstant.Contact.Email] = notification.Email });

            await _contactWrapper.UpdateContactAsync(notification.Email,
                request);
        }
    }
}