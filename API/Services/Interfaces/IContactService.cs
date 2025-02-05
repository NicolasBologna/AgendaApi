using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Dtos;

namespace AgendaApi.Services.Interfaces
{
    public interface IContactService : IExportable
    {
        ContactDto Create(CreateAndUpdateContactDto dto, int loggedUserId);
        void Delete(int id);
        List<ContactWithGroupIdsDto> GetAllByUser(int id);
        ContactWithGroupIdsDto? GetOneByUser(int userId, int contactId);
        bool ToggleFavorite(int contactId);
        void Update(CreateAndUpdateContactDto dto, int contactId);
    }
}