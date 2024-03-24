using Microsoft.EntityFrameworkCore;
using BerryMVC.Models;

namespace BerryMVC.Data
{
    public class BerryMVCContext : DbContext
    {
        public BerryMVCContext (DbContextOptions<BerryMVCContext> options)
            : base(options)
        {
        }

        public DbSet<BerryModel> BerryModel { get; set; } = default!;
        public DbSet<UserModel> UserModels { get; set; } = default!;
    }
}
