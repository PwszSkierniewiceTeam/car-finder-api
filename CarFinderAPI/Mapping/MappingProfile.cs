using AutoMapper;
using CarFinderAPI.Dtos;
using CarFinderAPI.Models;

namespace CarFinderAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //model -> dto
            CreateMap<Car, CarDto>();
            //dto->model
            CreateMap<CarDto, Car>()
                .ForMember(m => m.Id, o => o.Ignore());
        }
    }
}