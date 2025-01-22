using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models.Dtos
{
    public record CreateAndUpdateGroupDto([Required] string Name, string? Description = null);
}
