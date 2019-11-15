using CarFinderAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderAPI.Models
{
    public class CarResponse
    {
        public bool ExactMatch { get; set; }
        public IEnumerable<CarDto> Cars { get; set; }
    }
}
