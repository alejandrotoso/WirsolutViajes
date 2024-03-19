namespace WirsolutViajes.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } 

        public ICollection<BrandVehicleType> BrandVehicleTypes { get; set; }
    }
}
