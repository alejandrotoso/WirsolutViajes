using Microsoft.EntityFrameworkCore;
using WirsolutViajes.Domain.Entities;

namespace WirsolutViajes.Infrastructure.Persistence.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureWirsolutViajes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });


            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(100); 
                entity.Property(e => e.CountryISOCode).HasMaxLength(2); 
                entity.Property(e => e.State).HasMaxLength(100); 
                entity.Property(e => e.DeactivationComment).HasMaxLength(255); 
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.LicensePlate).HasMaxLength(8);
                entity.Property(e => e.DeactivationComment).HasMaxLength(255); 

                entity.HasOne(c => c.BrandVehicleType)
                    .WithMany()
                    .HasForeignKey(c => c.BrandVehicleTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Model)
                    .WithMany()
                    .HasForeignKey(c => c.ModelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VehicleSubtype>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

        }
    }
}
