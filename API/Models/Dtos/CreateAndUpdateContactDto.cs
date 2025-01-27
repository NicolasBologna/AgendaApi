using AgendaApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models
{
    public record CreateAndUpdateContactDto([Required] string FirstName, string? LastName = null, string? Address = null, string? Number = null, string? Email = null, string? Image = null, string? Company = null, string? Description = null);
}
