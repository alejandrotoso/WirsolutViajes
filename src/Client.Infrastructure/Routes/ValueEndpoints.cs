namespace WirsolutViajes.Client.Infrastructure.Routes
{
    public static class ValueEndpoints
    {
        public static string GetAllVehicleTypes = "api/values/all-vehicle-types";

        public static string GetVehicleSubtypesByVehicleTypeId(int vehicleTypeId)
        {
            return $"api/values/vehicle-subtypes-by-vehicle-type-id/{vehicleTypeId}";
        }

        public static string GetBrandsByVehicleTypeId(int vehicleTypeId)
        {
            return $"api/values/brands-by-vehicle-type-id/{vehicleTypeId}";
        }

        public static string GetModelsByBrandVehicleTypeId(int brandId, int vehicleTypeId)
        {
            return $"api/values/models-by-brand-id-and-vehicle-type-id/{brandId}/{vehicleTypeId}";
        }

        public static string GetBrandVehicleTypeById(int id)
        {
            return $"api/values/brand-vehicle-type-by-id/{id}";
        }
    }
}
