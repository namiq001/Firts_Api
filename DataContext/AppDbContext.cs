using First_Api.Entitys;
using Microsoft.EntityFrameworkCore;

namespace First_Api.DataContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
}
