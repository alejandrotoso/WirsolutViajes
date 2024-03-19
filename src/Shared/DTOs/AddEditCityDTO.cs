namespace WirsolutViajes.Shared.DTOs
{
    public class AddEditCityDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string CountryISOCode { get; set; }
        public string State { get; set; }
        public bool Active { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public string? DeactivationComment { get; set; }

        public string Description => $"{Name} - {State} - {Country}";
    }

}
