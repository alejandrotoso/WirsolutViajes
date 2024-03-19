namespace WirsolutViajes.Domain.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime TripDate { get; set; }
        public int DestinationId { get; set; }
        public int VehicleId { get; set; }
        public City Destination { get; set; }
        public Vehicle Vehicle { get; set; }
    }

}
