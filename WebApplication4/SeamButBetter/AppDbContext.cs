using Microsoft.EntityFrameworkCore;

namespace WebApplication4.SeamButBetter
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