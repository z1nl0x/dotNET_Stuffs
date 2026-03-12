using System.ComponentModel.DataAnnotations;

namespace PostgresCrud.Application.DTOs.User;

public record RegisterRequest(
    [Required] string Username,
    [Required][EmailAddress] string Email,
    [Required][MinLength(6)] string Password,
    string Role = "User"
);