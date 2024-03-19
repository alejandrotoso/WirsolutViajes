namespace WirsolutViajes.Client.Infrastructure.Routes
{
    public static class TripEndpoints
    {
        public static string Delete = "api/travels";
        public static string Save = "api/travels";

        public static string GetAll(DateTime date, int destinationId, int vehicleTypeId)
        {
            string dateF = date.ToString("yyyy-MM-dd");

            string urlParams = $"api/travels/all?" +
                               $"destinationId={destinationId}&" +
                               $"vehicleTypeId={vehicleTypeId}&" +
                               $"tripDate={dateF}";

            return urlParams;
        }
    }
}
