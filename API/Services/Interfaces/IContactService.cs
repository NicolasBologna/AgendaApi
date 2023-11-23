using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Dtos;

namespace AgendaApi.Services.Interfaces
{
    public interface IContactService
    {
        void Create(CreateAndUpdateContact dto, int loggedUserId);
        void Delete(int id);
        List<ContactDto> GetAllByUser(int id);
        ContactDto? GetOneByUser(int userId, int contactId);
        void Update(CreateAndUpdateContact dto, int contactId);
    }
}