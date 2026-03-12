namespace PostgresCrud.Application.DTOs.User;

public record AuthResponse(
    string Token,
    string Username,
    string Email,
    string Role
);