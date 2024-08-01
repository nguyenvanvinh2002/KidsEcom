using KidsEcomAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsEcomAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options):base(options) { }

        public DbSet<Products>Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
