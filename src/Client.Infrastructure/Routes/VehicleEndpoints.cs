namespace WirsolutViajes.Client.Infrastructure.Routes
{
    public static class VehicleEndpoints
    {
        public static string Save = "api/vehicles";


        public static string GetAll = "api/vehicles/all";

        public static string GetByTypeAndVehicleSubtypeId(int vehicleTypeId, int vehicleSubtypeId)
        {
            return $"api/vehicles/by-type-and-subtype-vehicle/{vehicleTypeId}/{vehicleSubtypeId}";
        }

        public static string GetById(int id)
        {
            return $"api/vehicles/get-by-id/{id}";
        }
    }
}
