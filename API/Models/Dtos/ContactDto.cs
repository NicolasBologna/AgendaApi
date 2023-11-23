using AgendaApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApi.Models.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Number { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? Company { get; set; }
        public string Description { get; set; } = String.Empty;
        public int UserId { get; set; }
    }
}
