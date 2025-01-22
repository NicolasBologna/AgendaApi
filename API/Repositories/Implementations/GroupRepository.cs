using AgendaApi.Data;
using AgendaApi.Entities;
using AgendaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Repositories.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AgendaContext _context;

        public GroupRepository(AgendaContext context)
        {
            _context = context;
        }

        public IEnumerable<Group> GetAllByOwner(int ownerId)
        {
            return _context.Groups.Where(g => g.OwnerId == ownerId);
        }

        public Group? GetOneByOwner(int ownerId, int groupId)
        {
            var group = _context.Groups.Include(g => g.Contacts).Include(g => g.Owner).FirstOrDefault(g => g.OwnerId == ownerId && g.Id == groupId);
            return group;
        }

        public Group? GetByGroupId(int groupId)
        {
            var group = _context.Groups.Include(g => g.Contacts).Include(g => g.Owner).FirstOrDefault(g => g.Id == groupId);
            return group;
        }

        public int Create(Group newGroup)
        {
            _context.Groups.Add(newGroup);
            _context.SaveChanges();
            return newGroup.Id;
        }

        public void Update(Group updatedGroup, int groupId)
        {
            var group = _context.Groups.SingleOrDefault(g => g.Id == groupId);
            if (group is not null)
            {
                group.Name = updatedGroup.Name;
                group.Description = updatedGroup.Description;
                group.OwnerId = updatedGroup.OwnerId;
                group.Contacts = updatedGroup.Contacts;
                _context.SaveChanges();
            }
        }

        public void Delete(int groupId)
        {
            var groupToDelete = _context.Groups.SingleOrDefault(g => g.Id == groupId);
            if (groupToDelete is not null)
            {
                _context.Groups.Remove(groupToDelete);
                _context.SaveChanges();
            }
        }
    }
}
