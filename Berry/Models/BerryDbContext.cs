using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Berry.DB
{
    public class BerryDbContext : DbContext
    {
        public DbSet<UserEntry> Users { get; set; }
        public DbSet<BerryEntry> Berries { get; set; }

        public BerryDbContext (DbContextOptions<BerryDbContext> ctx) : base(ctx)
        {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntry>().ToTable("Users");
            modelBuilder.Entity<BerryEntry>().ToTable("Berries");
        }
    }
}
