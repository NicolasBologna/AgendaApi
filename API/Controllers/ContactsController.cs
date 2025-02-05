using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            return Ok(_contactService.GetAllByUser(userId));
        }

        [HttpGet("{contactId}")]
        public IActionResult GetOne(int contactId)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            return Ok(_contactService.GetOneByUser(userId, contactId)); //Revisar 204
        }


        [HttpPost]
        public IActionResult CreateContact(CreateAndUpdateContactDto createContactDto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            var newContact = _contactService.Create(createContactDto, userId);
            return Created("Created", newContact);
        }

        [HttpPut]
        [Route("{contactId}")]
        public IActionResult UpdateContact(CreateAndUpdateContactDto dto, int contactId)
        {
            _contactService.Update(dto, contactId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{contactId}")]
        public IActionResult Delete(int contactId)
        {
            _contactService.Delete(contactId);
            return NoContent();
        }

        [HttpGet]
        [Route("export")]
        public IActionResult Export()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            var result = _contactService.Export(userId);

            if (!string.IsNullOrEmpty(result))
                return Ok(result);

            return BadRequest("El usuario no tiene ningún contacto");
        }

        [HttpPost]
        [Route("{contactId}/favorite")]
        public IActionResult MarkAsFavorite(int contactId)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            var contact = _contactService.GetOneByUser(userId, contactId);
            if (contact == null)
            {
                return NotFound(new { Message = "El contacto no se encontró o no pertenece al usuario en sesión" });
            }

            var newStatus = _contactService.ToggleFavorite(contactId);

            return Ok($"Contacto actualizado correctamente. El nuevo estado es: {newStatus}");
        }
    }
}
