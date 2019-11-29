namespace CarFinderAPI.Models
{
    public class CarRequest
    {
        public string BodyType { get; set; }
        public string Company { get; set; }
        public double? CapacityFrom { get; set; }
        public double? CapacityTo { get; set; }
        public string DriveType { get; set; }
        public string Fuel { get; set; }
        public double? FuelConsumptionTo { get; set; }
        public double? FuelConsumptionFrom { get; set; }
        public int? PowerFrom { get; set; }
        public int? PowerTo { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string Transmission { get; set; }
    }
}