using CarFinderAPI.Dtos;
using System.Collections.Generic;

namespace CarFinderAPI.Models
{
    public class CarResponse
    {
        public bool ExactMatch { get; set; }
        public IEnumerable<CarDto> Cars { get; set; }
    }
}