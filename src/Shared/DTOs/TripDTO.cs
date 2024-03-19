namespace WirsolutViajes.Shared.DTOs
{
    public class TripDTO
    {
        public int Id { get; set; }
        public DateTime TripDate { get; set; }
        public int DestinationId { get; set; }
        public int VehicleId { get; set; }
        public CityDTO Destination { get; set; }
        public VehicleDTO Vehicle { get; set; }
    }

}
