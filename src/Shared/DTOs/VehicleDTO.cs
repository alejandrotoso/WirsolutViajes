namespace WirsolutViajes.Shared.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public int BrandVehicleTypeId { get; set; }
        public int VehicleSubtypeId { get; set; }
        public int ModelId { get; set; }
        public int YearManufactured { get; set; }
        public int Mileage { get; set; }
        public BrandVehicleTypeDTO BrandVehicleType { get; set; }
        public VehicleSubtypeDTO VehicleSubtype { get; set; }
        public ModelDTO Model { get; set; }
        public bool Active { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public string? DeactivationComment { get; set; }

        public string Description
        {
            get
            {
                string subtypeName = VehicleSubtype != null ? VehicleSubtype.Name : "Sin subtipo";
                string brandName = BrandVehicleType != null && BrandVehicleType.Brand != null ? BrandVehicleType.Brand.Name : "Sin marca";
                string modelName = Model != null ? Model.Name : "Sin modelo";
                return $"{subtypeName} - {brandName} - {modelName} - {LicensePlate} | Año: {YearManufactured} | Km: {Mileage:n0}";
            }
        }
    }
}
