using PostgresCrud.Application.DTOs.User;

namespace PostgresCrud.Application.Services.Users;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}