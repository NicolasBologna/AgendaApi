using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models
{
    public record AuthenticationRequestDto([Required] string Email, [Required] string Password);
}
