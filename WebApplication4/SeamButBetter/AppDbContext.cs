using Microsoft.EntityFrameworkCore;

namespace WebApplication4
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Context> Contexts { get; set; }
        //public DbSet<Product> Products { get; set; }
    }
}