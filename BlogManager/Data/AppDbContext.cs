using BlogManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Data;

public class AppDbContext : DbContext
{
    public DbSet<BlogPost>? BlogPosts { get; set; }
    
    public DbSet<Tag>? Tags { get; set; }
    
    public DbSet<Author>? Authors { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("Data Source=blog.db; Cache=Shared");
}