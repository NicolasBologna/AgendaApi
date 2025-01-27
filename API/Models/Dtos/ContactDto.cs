﻿using AgendaApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApi.Models.Dtos
{
    public record ContactDto(int Id, string FirstName, string LastName, string? Address, string? Number, string? Email, string? Image, string? Company, string Description, int UserId, bool IsFavorite);
}
