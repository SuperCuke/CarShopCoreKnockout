using Microsoft.EntityFrameworkCore;
using System;

namespace CarShop.Model
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        { }

        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<CarFeatureModel> CarFeatureModels { get; set; }
        public DbSet<CarOrder> CarOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarModel>()
               .HasOne<CarCompany>(s => s.CarCompany)
               .WithMany(c => c.CarModels);

            modelBuilder.Entity<CarFeatureModel>()
            .HasOne<CarModel>(s => s.CarModel)
            .WithMany(m => m.SupportedFeatures);

            modelBuilder.Entity<CarFeatureModel>()
            .HasOne<CarFeature>(s => s.CarFeature)
            .WithMany();

            modelBuilder.Entity<CarOrder>()
             .HasOne<CarModel>(s => s.CarModel)
             .WithMany();

            modelBuilder.Entity<CarOrderFeature>()
            .HasOne<CarOrder>(cf => cf.CarOrder)
            .WithMany(b => b.CarFeatures)
            .HasForeignKey(bc => bc.OrderId);

            modelBuilder.Entity<CarOrderFeature>()
            .HasOne<CarFeatureModel>(cf => cf.CarFeatureModel)
            .WithMany()
            .HasForeignKey(bc => bc.FeatureModelId);
        }

    }
}
