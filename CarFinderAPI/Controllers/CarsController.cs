using AutoMapper;
using CarFinderAPI.Data;
using CarFinderAPI.Dtos;
using CarFinderAPI.Models;
using CarFinderAPI.Services;
using CarFinderAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private NotExactMatch _notExactMatch;

        public CarsController(ApplicationDbContext context, IMapper mapper, NotExactMatch notExactMatch)
        {
            _context = context;
            _mapper = mapper;
            _notExactMatch = notExactMatch;
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

            var alternativeSearch = _notExactMatch.CheckForResult(carRequest);
            if (alternativeSearch.Any())
            {
                return Ok(new CarResponse
                {
                    ExactMatch = false,
                    Cars = alternativeSearch.ToList().Select(_mapper.Map<Car, CarDto>)
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