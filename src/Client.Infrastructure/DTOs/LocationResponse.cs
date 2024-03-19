namespace WirsolutViajes.Client.Infrastructure.DTOs
{
    public class LocationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string State { get; set; }

        public string Description => $"{Name} - {State} - {CountryName}";
    }
}
