using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Dtos;
using AgendaApi.Models.Enum;
using AgendaApi.Repositories.Interfaces;
using AgendaApi.Services.Interfaces;

namespace AgendaApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public GetUserByIdDto? GetById(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user is not null)
            {
                return new GetUserByIdDto(user.Id, user.FirstName, user.LastName, user.Email, user.State);
            }
            return null;
        }

        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            User? result = null;

            if (!string.IsNullOrEmpty(authRequestBody.UserName) && !string.IsNullOrEmpty(authRequestBody.Password)) //verifico que no sean null (no deberían por definición) ni que sea un string vacío
                result = _userRepository.ValidateUser(new AuthenticationRequestDto(authRequestBody.UserName, authRequestBody.Password));
            return result;
        }

        public IEnumerable<UserDto> GetAll()
        {
            //Acá hacemos un select para convertir todas las entidades User a GetUserByIdDto para no mandar todos los Contacts de cada user ni tampoco la contraseña y solo enviar la info básica del usuario.
            return _userRepository.GetAll().Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email, u.State));
        }

        public int Create(CreateAndUpdateUserDto dto)
        {
            User newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                Email = dto.Email,
                State = State.Active,
                Contacts = []
            };
            return _userRepository.Create(newUser);
        }

        public void Update(CreateAndUpdateUserDto dto, int userId)
        {
            var user = new User()
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Password = dto.Password,
            };
            _userRepository.Update(user, userId);
        }

        public void RemoveUser(int userId)
        {
            _userRepository.RemoveUser(userId);
        }

        public bool CheckIfUserExists(int userId)
        {
            return _userRepository.CheckIfUserExists(userId);
        }
    }
}
