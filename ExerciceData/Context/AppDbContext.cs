using ExerciceData.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciceData.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<TirModel> Tirs { get; set; }
        public DbSet<DriverModel> Drivers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DriverModel>()
                .HasOne(driver => driver.Tir)
                .WithOne(tir => tir.Driver)
                .HasForeignKey<DriverModel>(driver => driver.TirId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<TirModel>()
                .HasOne(tir => tir.Company)
                .WithMany(company => company.Tirs)
                .HasForeignKey(tir => tir.CompanyId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<DriverModel>()
                .HasOne(driver => driver.Company)
                .WithMany(company => company.Drivers)
                .HasForeignKey(driver => driver.CompanyId)
                .OnDelete(DeleteBehavior.NoAction); 
        }

    }
}
