using AgendaApi.Entities;
using AgendaApi.Models.Dtos;
using AgendaApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // Obtener todos los grupos del usuario autenticado
        [HttpGet]
        public IActionResult GetAll()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            return Ok(_groupService.GetAllByUser(userId));
        }

        // Obtener un grupo específico por ID
        [HttpGet("{groupId}")]
        public IActionResult GetOne(int groupId)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            var group = _groupService.GetOneByUser(userId, groupId);
            if (group == null)
            {
                return NotFound(new { Message = "El grupo no se encontró o no pertenece al usuario en sesión" });
            }

            return Ok(group);
        }

        // Crear un nuevo grupo
        [HttpPost]
        public IActionResult CreateGroup(CreateAndUpdateGroupDto createGroupDto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            _groupService.Create(createGroupDto, userId);
            return Created("Created", createGroupDto);
        }

        // Actualizar un grupo existente
        [HttpPut]
        [Route("{groupId}")]
        public IActionResult UpdateGroup(CreateAndUpdateGroupDto dto, int groupId)
        {
            _groupService.Update(dto, groupId);
            return NoContent();
        }

        // Agregar o eliminar un contacto al grupo
        [HttpPut]
        [Route("{groupId}/contact/{contactId}")]
        public IActionResult AddOrRemoveContact(int groupId, int contactId)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            _groupService.AddOrRemoveContact(groupId, contactId, userId);
            return NoContent();
        }

        // Eliminar un grupo
        [HttpDelete]
        [Route("{groupId}")]
        public IActionResult Delete(int groupId)
        {
            _groupService.Delete(groupId);
            return NoContent();
        }

        // Exportar los grupos del usuario autenticado
        [HttpGet]
        [Route("export/{groupId}")]
        public IActionResult Export(int groupId)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            var result = _groupService.Export(groupId);

            if (!string.IsNullOrEmpty(result))
                return Ok(result);

            return NotFound();
        }
    }
}
