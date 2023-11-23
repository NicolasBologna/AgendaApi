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
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactController(IContactService contactService, IUserService userRepository)
        {
            _contactService = contactService;
            _userService = userRepository;
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
            return Ok(_contactService.GetOneByUser(userId, contactId));
        }


        [HttpPost]
        public IActionResult CreateContact(CreateAndUpdateContact createContactDto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            _contactService.Create(createContactDto, userId);
            return Created("Created", createContactDto);
        }

        [HttpPut]
        [Route("{contactId}")]
        public IActionResult UpdateContact(CreateAndUpdateContact dto, int contactId)
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

    }
}
