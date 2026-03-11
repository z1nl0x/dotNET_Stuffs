using Microsoft.EntityFrameworkCore;
using PostgresCrud.Entities;

namespace PostgresCrud.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :  base(options) { }
    
    public DbSet<Product> Products { get; set; }
}