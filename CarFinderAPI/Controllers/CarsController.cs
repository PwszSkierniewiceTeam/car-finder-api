using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CarFinderAPI.Data;
using CarFinderAPI.Dtos;
using CarFinderAPI.Models;
using CarFinderAPI.Utilities;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<CarResponse> Get([FromBody] CarRequest carRequest)
        {
            var predicate = CarPredicateBuilder.BuildPredicate(carRequest);

            var result = _context.Cars.Where(predicate);
            if (result.Any())
            {
                return Ok(new CarResponse
                {
                    ExactMatch = true,
                    Cars = result.ToList().Select(_mapper.Map<Car, CarDto>)
                });
            }

            var type = typeof(CarRequest);
            var propertyInfos = type.GetTypeInfo().GetProperties(BindingFlags.Public);
            

            var alternativeSearch = true; //for test only
            if (alternativeSearch == true)
            {
                return Ok(new CarResponse
                {
                    ExactMatch = false,
                    Cars = new List<CarDto>()
                });
            }

            return NotFound();
        }

        //[HttpPost]
        //public ActionResult Post([FromBody] CarDto[] cars)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    _context.Cars.AddRange(cars.Select(_mapper.Map<CarDto, Car>));
        //    _context.SaveChanges();

        //    return Ok();
        //}
    }
}