namespace WirsolutViajes.Shared.DTOs
{
    public class AddEditTripDTO
    {
        public int Id { get; set; }
        public DateTime? TripDate { get; set; }
        public int DestinationId { get; set; }
        public int VehicleId { get; set; }
    }

}
