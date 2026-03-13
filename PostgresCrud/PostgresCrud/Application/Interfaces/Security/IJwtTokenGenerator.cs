using PostgresCrud.Domain.User;

namespace PostgresCrud.Application.Interfaces.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}