using Microsoft.EntityFrameworkCore;
using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Infrastructure.Persistence.Contexts;

namespace WirsolutViajes.Server.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleSubtype> VehicleSubtypes { get; set; }
        public DbSet<BrandVehicleType> BrandVehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Trip> Trips { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);


            builder.ConfigureWirsolutViajes();

        }
    }
}
