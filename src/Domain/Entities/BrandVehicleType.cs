namespace WirsolutViajes.Domain.Entities
{
    public class BrandVehicleType
    {
        public int Id { get; set; }

        public int BrandId { get; set; }
        public int VehicleTypeId { get; set; }
        public Brand Brand { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
