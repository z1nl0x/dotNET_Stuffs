using Microsoft.EntityFrameworkCore;
using PostgresCrud.Entities;

namespace PostgresCrud.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :  base(options) { }
    
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasPostgresExtension("pgcrypto");
        
        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();
    }
}