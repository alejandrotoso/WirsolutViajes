using System.Reflection;

namespace WirsolutViajes.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; } 
        public string LicensePlate { get; set; } 
        public int BrandVehicleTypeId { get; set; } 
        public int VehicleSubtypeId { get; set; } 
        public int ModelId { get; set; }
        public int YearManufactured { get; set; }
        public int Mileage { get; set; } 
        public BrandVehicleType BrandVehicleType { get; set; }
        public VehicleSubtype VehicleSubtype { get; set; }
        public Model Model { get; set; }
        public bool Active { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public string? DeactivationComment { get; set; }
    }
}
