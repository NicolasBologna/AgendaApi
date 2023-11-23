using AgendaApi.Data;
using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Dtos;
using AgendaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly AgendaContext _context;

        public ContactService(AgendaContext context)
        {
            _context = context;
        }
        public List<ContactDto> GetAllByUser(int id)
        {

            return _context.Contacts.Include(c => c.User).Where(c => c.User.Id == id).Select(contact => new ContactDto()
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

            var contact = _context.Contacts.Include(c => c.User).FirstOrDefault(c => c.User.Id == userId && c.Id == contactId);
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
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public void Update(CreateAndUpdateContact dto, int contactId)
        {
            Contact? contact = _context.Contacts.SingleOrDefault(contact => contact.Id == contactId);
            if (contact is not null)
            {
                contact.Email = dto.Email;
                contact.Image = dto.Image;
                contact.Number = dto.Number;
                contact.Company = dto.Company;
                contact.Address = dto.Address;
                contact.LastName = dto.LastName;
                contact.FirstName = dto.FirstName;
                _context.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var userToDelete = _context.Contacts.Single(c => c.Id == id);
            if (userToDelete is not null) { 
                _context.Contacts.Remove(userToDelete);
            }
            _context.SaveChanges();
        }
    }
}
