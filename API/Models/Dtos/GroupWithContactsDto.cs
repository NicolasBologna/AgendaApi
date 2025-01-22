namespace AgendaApi.Models.Dtos
{
    public record GroupWithContactsDto(int Id, string Name, string? Description, int OwnerId, List<ContactDto> Contacts);
}
