using PostgresCrud.Application.DTOs.User;
using PostgresCrud.Application.Interfaces.Repositories;
using PostgresCrud.Application.Interfaces.Security;
using PostgresCrud.Domain.User;

namespace PostgresCrud.Application.Services.Users;

public class AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await userRepository.ExistsAsync(request.Email))
            throw new InvalidOperationException("Email already in use.");
        
        var role = request.Role == UserRoles.Admin ? UserRoles.Admin : UserRoles.User;

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = role
        };

        await userRepository.AddAsync(user);

        return new AuthResponse(
            Token: jwtTokenGenerator.GenerateToken(user),
            Username: user.Username,
            Email: user.Email,
            Role: user.Role
        );
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        return new AuthResponse(
            Token: jwtTokenGenerator.GenerateToken(user),
            Username: user.Username,
            Email: user.Email,
            Role: user.Role
        );
    }

    
}