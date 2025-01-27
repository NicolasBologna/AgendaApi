using AgendaApi.Models;
using AgendaApi.Models.Dtos;
using AgendaApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpGet]
        public ActionResult<UserDto> GetAll()
        {
            //No lo estamos verificando, pero por lo general un GetAll de todos los users lo debería poder hacer solo un usuario con rol ADMIN
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOneById(int id)
        {
            if (id == 0)
            {
                return BadRequest("El ID ingresado debe ser distinto de 0");
            }

            GetUserByIdDto? user = _userService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);

        }

        [HttpPost]
        [AllowAnonymous] //Esto lo agregamos porque en nuestro caso el create user lo vamos a usar para el registro (queremos saltear la autenticación)
        public IActionResult CreateUser(CreateAndUpdateUserDto dto)
        {
            try
            {
                _userService.Create(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Created("Created", dto);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(CreateAndUpdateUserDto dto, int userId)
        {
            if (!_userService.CheckIfUserExists(userId))
            {
                return NotFound();
            }
            try
            {
                _userService.Update(dto, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.RemoveUser(id);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("me")]
        public ActionResult<GetUserByIdDto> GetUserInfo()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            var user = _userService.GetById(userId);
            return Ok(user);
        }
    }
}
