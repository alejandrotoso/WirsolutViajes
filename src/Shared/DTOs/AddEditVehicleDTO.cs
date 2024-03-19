namespace WirsolutViajes.Shared.DTOs
{
    public class AddEditVehicleDTO
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public int BrandVehicleTypeId { get; set; }
        public string VehicleSubtypeId { get; set; }
        public string ModelId { get; set; }
        public int YearManufactured { get; set; }
        public int Mileage { get; set; }
        public bool Active { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public string? DeactivationComment { get; set; }
    }
}
