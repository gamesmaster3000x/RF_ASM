using Microsoft.EntityFrameworkCore;

namespace Berry.DB
{
    public class BerryDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dependency> Dependencies { get; set; }

        public BerryDBContext (DbContextOptions<BerryDBContext> ctx) : base(ctx)
        {
        }
    }
}
