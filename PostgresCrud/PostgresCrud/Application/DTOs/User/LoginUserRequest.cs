using System.ComponentModel.DataAnnotations;

namespace PostgresCrud.Application.DTOs.User;

public record LoginRequest(
    [Required] string Email,
    [Required] string Password
);