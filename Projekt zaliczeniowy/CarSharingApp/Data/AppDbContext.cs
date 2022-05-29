using CarSharingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Color> Colors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(a => a.CarBrand)
                .WithMany(b => b.Car)
                .HasForeignKey(a => a.CarBrandId);
            modelBuilder.Entity<Car>()
                .HasOne(a => a.CarModel)
                .WithMany(b => b.Car)
                .HasForeignKey(a => a.CarModelId);
            modelBuilder.Entity<Car>()
                .HasOne(a => a.Color)
                .WithMany(b => b.Car)
                .HasForeignKey(a => a.ColorId);
        }
    }
}
