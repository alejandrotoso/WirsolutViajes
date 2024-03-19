namespace WirsolutViajes.Domain.Entities
{
    public class VehicleSubtype
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public int VehicleTypeId { get; set; } 
        public VehicleType VehicleType { get; set; }
    }
}
