using AgendaApi.Data;
using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Enum;
using AgendaApi.Repositories.Interfaces;

namespace AgendaApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private AgendaContext _context;
        public UserRepository(AgendaContext context)
        {
            _context = context;
        }
        public User? GetById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public int Create(User newUser)
        {
            var createdUser = _context.Users.Add(newUser).Entity; //Cuando creamos el usuario se genera una nueva instancia que contiene los nuevos datos que crea la Base, como por el ejemplo el ID.
            _context.SaveChanges();
            return createdUser.Id;
        }

        //El update funciona de la siguiente manera:
        /*
         * Primero traemos la entidad de la base de datos.
         * Cuando traemos la entidad entity framework trackea las propiedades del objeto
         * Cuando modificamos algo el estado de la entidad pasa a "Modified"
         * Una vez hacemos _context.SaveChanges() esto va a ver que la entidad fue modificada y guarda los cambios en la base de datos.
         */
        public void Update(User updatedUser, int userId)
        {
            User userToUpdate = _context.Users.First(u => u.Id == userId);
            userToUpdate.FirstName = updatedUser.FirstName;
            //userToUpdate.UserName = dto.NombreDeUsuario; //Esto no deberíamos actualizarlo, lo mejor es crear un dto para actualización que no contenga este campo.
            userToUpdate.LastName = updatedUser.LastName;
            userToUpdate.Password = updatedUser.Password;
            _context.SaveChanges();
        }

        public void RemoveUser(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new Exception("El cliente que intenta eliminar no existe");
            }

            if (user.FirstName != "Admin")
            {
                Delete(userId);
            }
            else
            {
                Archive(userId);
            }
        }

        private void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            _context.SaveChanges();
        }

        private void Archive(int id)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.State = State.Archived;
            }
            _context.SaveChanges();
        }

        public bool CheckIfUserExists(int userId)
        {
            return _context.Users.Any(user => user.Id == userId);
        }
    }
}
