using AgendaApi.Models.Dtos;

namespace AgendaApi.Services.Interfaces
{
    public interface IGroupService : IExportable
    {
        void Create(CreateAndUpdateGroupDto dto, int loggedUserId);
        void Delete(int groupId);
        List<GroupDto> GetAllByUser(int userId);
        GroupWithContactsDto? GetOneByUser(int userId, int groupId);
        void Update(CreateAndUpdateGroupDto dto, int groupId);
        bool AddOrRemoveContact(int GroupId, int ContactId, int UserId);
    }
}