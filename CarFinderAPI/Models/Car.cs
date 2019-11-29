using System.ComponentModel.DataAnnotations;

namespace CarFinderAPI.Models
{
    public class Car
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(32)]
        public string BodyType { get; set; }

        public double? Capacity { get; set; }

        [MaxLength(32)]
        public string Company { get; set; }

        [MaxLength(32)]
        public string DriveType { get; set; }

        [MaxLength(32)]
        public string Fuel { get; set; }

        public double? FuelConsumption { get; set; }

        [MaxLength]
        public string ImageUrl { get; set; }

        [MaxLength(32)]
        public string Model { get; set; }

        public int? Power { get; set; }
        public int? Price { get; set; }

        [MaxLength(32)]
        public string Transmission { get; set; }

        [MaxLength(32)]
        public string Version { get; set; }
    }
}