namespace WirsolutViajes.Domain.Entities
{
    public class VehicleType
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public ICollection<BrandVehicleType> BrandsVehicleTypes { get; set; }
    }
}
