using AgendaApi.Data;
using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Dtos;
using AgendaApi.Repositories.Implementations;
using AgendaApi.Repositories.Interfaces;
using AgendaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public List<ContactDto> GetAllByUser(int id)
        {

            return _contactRepository.GetAllByUser(id).Select(contact => new ContactDto()
            {
                Id = contact.Id,
                Address = contact.Address,
                Company = contact.Company,
                Description = contact.Description,
                Email = contact.Email,
                Image = contact.Image,
                LastName = contact.LastName,
                FirstName = contact.FirstName,
                Number = contact.Number,
                UserId = contact.UserId
            }).ToList();
        }

        public ContactDto? GetOneByUser(int userId, int contactId)
        {

            var contact = _contactRepository.GetOneByUser(userId, contactId);
            if (contact is not null)
            {
                return new ContactDto()
                {
                    Id = contact.Id,
                    Address = contact.Address,
                    Company = contact.Company,
                    Description = contact.Description,
                    Email = contact.Email,
                    Image = contact.Image,
                    LastName = contact.LastName,
                    FirstName = contact.FirstName,
                    Number = contact.Number,
                    UserId = contact.UserId
                };
            }
            return null;
        }

        public void Create(CreateAndUpdateContact dto, int loggedUserId)
        {
            Contact contact = new Contact()
            {
                Email = dto.Email,
                Image = dto.Image,
                Number = dto.Number,
                Company = dto.Company,
                Address = dto.Address,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                UserId = loggedUserId,
            };
            _contactRepository.Create(contact);
        }

        public void Update(CreateAndUpdateContact dto, int contactId)
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

            // Generamos un encabezado para los datos exportados.
            string header = "Id,FirstName,LastName,Address,Number,Email,Image,Company,Description,UserId\n";

            // Usamos Aggregate para concatenar los datos de los contactos en formato CSV.
            string contactData = contacts.Aggregate(header, (result, contact) =>
                result +
                $"{contact.Id},{contact.FirstName},{contact.LastName},{contact.Address},{contact.Number},{contact.Email},{contact.Image},{contact.Company},{contact.Description},{contact.UserId}\n"
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
