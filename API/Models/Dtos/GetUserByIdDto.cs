using AgendaApi.Models.Enum;

namespace AgendaApi.Models
{
    //Acá usamos un dto que creamos para esta consulta sin Contacts, ya que no queremos que nos quede User -> Contact -> User -> Contact, etc
    public record GetUserByIdDto(int Id, string FirstName, string LastName, string UserName, State State, Role Role);
}
