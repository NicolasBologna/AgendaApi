using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Records;

namespace AgendaApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool CheckIfUserExists(int userId);
        int Create(User newUser);
        List<User> GetAll();
        User? GetById(int userId);
        void RemoveUser(int userId);
        void Update(User updatedUser, int userId);
        User? ValidateUser(LoginData authRequestBody);
    }
}