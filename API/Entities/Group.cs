using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApi.Entities
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public ICollection<Contact> Contacts { get; set; } = []; //Ojo! Uso ICollection y no IEnumerable para poder agregar/eliminar elementos con EF
    }
}
