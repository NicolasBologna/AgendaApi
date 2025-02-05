using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Dtos;
using AgendaApi.Repositories.Interfaces;
using AgendaApi.Services.Interfaces;

namespace AgendaApi.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IGroupRepository _groupRepository;

        public ContactService(IContactRepository contactRepository, IGroupRepository groupRepository)
        {
            _contactRepository = contactRepository;
            _groupRepository = groupRepository;
        }
        public List<ContactWithGroupIdsDto> GetAllByUser(int id)
        {
            return _contactRepository.GetAllByUser(id).Select(contact => new ContactWithGroupIdsDto(
                contact.Id,
                contact.FirstName,
                contact.LastName,
                contact.Address,
                contact.Number,
                contact.Email,
                contact.Image,
                contact.Company,
                contact.Description,
                contact.UserId,
                contact.IsFavorite,
                _groupRepository.GetGroupsByContactId(contact.Id)
               )
            ).ToList();
        }

        public ContactWithGroupIdsDto? GetOneByUser(int userId, int contactId)
        {

            var contact = _contactRepository.GetOneByUser(userId, contactId);
            if (contact is not null)
            {
                return new ContactWithGroupIdsDto(contact.Id,
                contact.FirstName,
                contact.LastName,
                contact.Address,
                contact.Number,
                contact.Email,
                contact.Image,
                contact.Company,
                contact.Description,
                contact.UserId,
                contact.IsFavorite,
                _groupRepository.GetGroupsByContactId(contact.Id));
            }
            return null;
        }

        public ContactDto Create(CreateAndUpdateContactDto dto, int loggedUserId)
        {
            Contact contact = new()
            {
                Email = dto.Email,
                Image = dto.Image,
                Number = dto.Number,
                Company = dto.Company,
                Address = dto.Address,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                UserId = loggedUserId,
                Description = dto.Description ?? "",
                IsFavorite = false
            };
            _contactRepository.Create(contact);

            return new ContactDto(
                contact.Id,
                contact.FirstName,
                contact.LastName,
                contact.Address,
                contact.Number,
                contact.Email,
                contact.Image,
                contact.Company,
                contact.Description,
                contact.UserId,
                contact.IsFavorite
            );
        }

        public void Update(CreateAndUpdateContactDto dto, int contactId)
        {
            Contact? contact = _contactRepository.GetByContactId(contactId);
            if (contact is not null)
            {
                contact.Email = dto.Email;
                contact.Image = dto.Image;
                contact.Number = dto.Number;
                contact.Company = dto.Company;
                contact.Address = dto.Address;
                contact.LastName = dto.LastName;
                contact.FirstName = dto.FirstName;
                contact.Description = dto.Description ?? "";

                _contactRepository.Update(contact, contactId);
            }

        }
        public void Delete(int id)
        {
            _contactRepository.Delete(id);
        }

        public string Export(int userId)
        {
            var contacts = _contactRepository.GetAllByUser(userId);

            if (contacts.Count() == 0)
            {
                return "";
            }

            // Generamos un encabezado para los datos exportados.
            string header = "FirstName,LastName,Address,Number,Email,Image,Company,Description\n";

            // Usamos Aggregate para concatenar los datos de los contactos en formato CSV.
            string contactData = contacts.Aggregate(header, (result, contact) =>
                result +
                $"{contact.FirstName},{contact.LastName},{contact.Address},{contact.Number},{contact.Email},{contact.Image},{contact.Company},{contact.Description}\n"
            );

            return contactData;
        }

        public bool ToggleFavorite(int contactId)
        {
            Contact? contact = _contactRepository.GetByContactId(contactId);
            if (contact is not null)
            {
                contact.IsFavorite = !contact.IsFavorite;

                _contactRepository.Update(contact, contactId);
            }
            else
            {
                throw new Exception("El contacto no se pudo actualizar");
            }

            return contact.IsFavorite;
        }
    }
}
