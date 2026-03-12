using PostgresCrud.Domain.User;

namespace PostgresCrud.Infrastructure.Repositories.Users;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsAsync(string email);
    Task AddAsync(User user);
}