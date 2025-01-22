using AgendaApi.Entities;

namespace AgendaApi.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        int Create(Group newGroup);
        void Delete(int groupId);
        IEnumerable<Group> GetAllByOwner(int ownerId);
        Group? GetByGroupId(int groupId);
        Group? GetOneByOwner(int ownerId, int groupId);
        void Update(Group updatedGroup, int groupId);
    }
}