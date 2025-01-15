using AgendaApi.Entities;

namespace AgendaApi.Repositories.Interfaces
{
    public interface IContactRepository
    {
        void Create(Contact newContact);
        void Delete(int id);
        List<Contact> GetAllByUser(int id);
        Contact? GetByContactId(int contactId);
        Contact? GetOneByUser(int userId, int contactId);
        void Update(Contact updatedContact, int contactId);
    }
}