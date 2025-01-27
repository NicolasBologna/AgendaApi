using AgendaApi.Entities;
using AgendaApi.Models.Dtos;
using AgendaApi.Repositories.Interfaces;
using AgendaApi.Services.Interfaces;

namespace AgendaApi.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IContactRepository _contactRepository;

        public GroupService(IGroupRepository groupRepository, IContactRepository contactRepository)
        {
            _groupRepository = groupRepository;
            _contactRepository = contactRepository;
        }

        public List<GroupDto> GetAllByUser(int userId)
        {
            return _groupRepository.GetAllByOwner(userId).Select(group => new GroupDto(
                group.Id,
                group.Name,
                group.Description,
                group.OwnerId
            )).ToList();
        }

        public GroupWithContactsDto? GetOneByUser(int userId, int groupId)
        {
            var group = _groupRepository.GetOneByOwner(userId, groupId);
            if (group != null)
            {
                return new GroupWithContactsDto(
                    group.Id,
                    group.Name,
                    group.Description,
                    group.OwnerId,
                    group.Contacts.Select(contact => new ContactDto(
                        contact.Id,
                        contact.FirstName,
                        contact.LastName,
                        contact.Address,
                        contact.Number,
                        contact.Email,
                        contact.Image,
                        contact.Company,
                        contact.Description,
                        contact.UserId
                    )).ToList()
                );
            }
            return null;
        }

        public void Create(CreateAndUpdateGroupDto dto, int loggedUserId)
        {
            Group group = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                OwnerId = loggedUserId
            };

            _groupRepository.Create(group);
        }

        public void Update(CreateAndUpdateGroupDto dto, int groupId)
        {
            Group? group = _groupRepository.GetByGroupId(groupId);
            if (group != null)
            {
                group.Name = dto.Name;
                group.Description = dto.Description;

                _groupRepository.Update(group, groupId);
            }
        }

        public void Delete(int groupId)
        {
            _groupRepository.Delete(groupId);
        }

        public string Export(int id)
        {
            var group = _groupRepository.GetByGroupId(id);

            if (group == null)
            {
                return "";
            }
            // Generamos un encabezado para los datos exportados.
            string header = "FirstName,LastName,Address,Number,Email,Image,Company,Description\n";

            // Usamos Aggregate para concatenar los datos de los contactos en formato CSV.
            string contactData = group.Contacts.Aggregate(header, (result, contact) =>
                result +
                $"{contact.FirstName},{contact.LastName},{contact.Address},{contact.Number},{contact.Email},{contact.Image},{contact.Company},{contact.Description}\n"
            );

            return contactData;
        }

        public bool AddOrRemoveContact(int groupId, int contactId, int userId)
        {
            var result = false;
            var group = _groupRepository.GetByGroupId(groupId);
            if (group != null)
            {
                if (group.OwnerId == userId)
                {
                    var contact = _contactRepository.GetOneByUser(userId, contactId);
                    if (contact != null)
                    {
                        if (group.Contacts.Contains(contact))
                        {
                            group.Contacts = group.Contacts.Where(c => c.Id != contactId).ToList(); //Se puede usar el método Remove, pero no me gusta porque puede dar problemas al no tener la referencia al objeto.
                        }
                        else
                        {
                            group.Contacts.Add(contact);
                        }
                        _groupRepository.Update(group, groupId);
                        result = true;
                    }
                }
            }

            return result;

        }
    }
}
