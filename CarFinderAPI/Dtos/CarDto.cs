using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderAPI.Dtos
{
    public class CarDto
    {
        public string BodyType { get; set; }
        public double? Capacity { get; set; }
        public string Company { get; set; }
        public string DriveType { get; set; }
        public string Fuel { get; set; }
        public double? FuelConsumption { get; set; }
        public string ImageUrl { get; set; }
        public string Model { get; set; }
        public int? Power { get; set; }
        public int? Price { get; set; }
        public string Transmission { get; set; }
        public string Version { get; set; }
    }
}
