using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApi.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public IEnumerable<int> Contacts { get; set; } = Enumerable.Empty<int>();
    }
}
