using AgendaApi.Data;
using AgendaApi.Entities;
using AgendaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Repositories.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly AgendaContext _context;
        public ContactRepository(AgendaContext context)
        {
            _context = context;
        }
        public IEnumerable<Contact> GetAllByUser(int id)
        {
            return _context.Contacts.Include(c => c.User).Where(c => c.User.Id == id);
        }
        public Contact? GetOneByUser(int userId, int contactId)
        {

            var contact = _context.Contacts.Include(c => c.User).FirstOrDefault(c => c.User.Id == userId && c.Id == contactId);
            if (contact is not null)
            {
                return contact;
            }
            return null;
        }

        public Contact? GetByContactId(int contactId)
        {

            var contact = _context.Contacts.Include(c => c.User).FirstOrDefault(c => c.Id == contactId);
            if (contact is not null)
            {
                return contact;
            }
            return null;
        }

        public void Create(Contact newContact)
        {
            _context.Contacts.Add(newContact);
            _context.SaveChanges();
        }

        public void Update(Contact updatedContact, int contactId)
        {
            Contact? contact = _context.Contacts.SingleOrDefault(contact => contact.Id == contactId);
            if (contact is not null)
            {
                contact.Email = updatedContact.Email;
                contact.Image = updatedContact.Image;
                contact.Number = updatedContact.Number;
                contact.Company = updatedContact.Company;
                contact.Address = updatedContact.Address;
                contact.LastName = updatedContact.LastName;
                contact.FirstName = updatedContact.FirstName;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var userToDelete = _context.Contacts.Single(c => c.Id == id);
            if (userToDelete is not null)
            {
                _context.Contacts.Remove(userToDelete);
            }
            _context.SaveChanges();
        }
    }
}
