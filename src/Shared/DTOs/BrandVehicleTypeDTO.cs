namespace WirsolutViajes.Shared.DTOs
{
    public class BrandVehicleTypeDTO
    {
        //public BrandVehicleTypeDTO()
        //{
        //    Brand = new BrandDTO();
        //    VehicleType = new VehicleTypeDTO();

        //}
        public int Id { get; set; }

        public int BrandId { get; set; }
        public int VehicleTypeId { get; set; }
        public BrandDTO Brand { get; set; }
        public VehicleTypeDTO VehicleType { get; set; }
    }

}
