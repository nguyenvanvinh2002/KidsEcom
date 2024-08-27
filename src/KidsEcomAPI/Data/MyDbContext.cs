using KidsEcomAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsEcomAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options):base(options) { }

        public DbSet<Products>Products { get; set; }
        public DbSet<DanhMucSanPhams> DanhMucs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Oders> Oders { get; set; }
        public DbSet<Thongbao> Thongbaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
