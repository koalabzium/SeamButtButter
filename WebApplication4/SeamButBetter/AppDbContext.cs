using Microsoft.EntityFrameworkCore;

namespace ConversationManager.SeamButBetter
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Context> Contexts { get; set; }
    }
}