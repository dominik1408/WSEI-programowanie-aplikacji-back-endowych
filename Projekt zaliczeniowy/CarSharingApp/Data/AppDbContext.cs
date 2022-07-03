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
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<User> Users { get; set; }   
        

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
            modelBuilder.Entity<Loan>()
                .HasOne(a => a.LoanType)
                .WithMany(b => b.Loan)
                .HasForeignKey(a => a.LoanTypeId);
            modelBuilder.Entity<Loan>()
                .HasOne(a => a.Car)
                .WithMany(b => b.Loan)
                .HasForeignKey(a => a.CarId);
            modelBuilder.Entity<Loan>()
                .HasOne(a => a.User)
                .WithMany(b => b.Loan)
                .HasForeignKey(a => a.UserId);
            modelBuilder.Entity<User>()
                .Property(a => a.Roles)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}
