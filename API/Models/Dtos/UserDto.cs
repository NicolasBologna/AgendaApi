using AgendaApi.Models.Enum;

namespace AgendaApi.Models.Dtos
{
    public record UserDto(int Id, string FirstName, string LastName, string Email, State State);
}
