using Microsoft.EntityFrameworkCore;
using PostgresCrud.Application.Interfaces.Repositories;
using PostgresCrud.Data;
using PostgresCrud.Domain.User;

namespace PostgresCrud.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<bool> ExistsAsync(string email)
        => await _context.Users.AnyAsync(u => u.Email == email);

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}